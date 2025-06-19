using GameServer.State;
using GameServer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedData;
using SharedData.Payloads;
using SharedData.Utils;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameServer;

public class GameManager : WebSocketBehavior
{
    private static Dictionary<string, Context> _gameContexts = new Dictionary<string, Context>();

    protected override void OnClose(CloseEventArgs e)
    {
        PlayersManagerInstance.GetInstance().RemovePlayer(this.ID);
        Sessions.Broadcast(GetPlayersList());
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var message = e.Data;
        WsEnvelope? envelope;

        try {
            envelope = JsonConvert.DeserializeObject<WsEnvelope>(message);
            if (!ValidateEnvelopeAndSendErrorIfNeeded(envelope)) return;
        }
        catch (JsonException ex) {
            Console.WriteLine("Deserialize error" + ex.Message);
            Sessions.SendTo("Message format error", this.ID);
            return;
        }

        if (envelope == null) {
            Console.WriteLine("An empty or invalid message was received from the customer " + this.ID);
            Sessions.SendTo("Invalid message", this.ID);
            return;
        }

        var context = GetOrCreateContext(envelope.GameId, Sessions);

        switch (envelope.ActionType) {
            case ActionType.InvitePlayer:
                context.SendInvitation(envelope.Player1Id, envelope.Player2Id);
                break;

            case ActionType.AcceptGame:
                context.CreateNewGame(envelope.Player1Id, envelope.Player2Id);
                break;

            case ActionType.GetShip:
                var payload = ExtractPayload<GetShipPayload>(envelope.Data);
                context.GetShip(payload!.X, payload!.Y);
                break;

            case ActionType.PlayerMove:
                var movingShip = ExtractPayload<PlayerMovePayload>(envelope.Data);
                context.ShipMoving(movingShip!.NewPosition);
                break;

            case ActionType.ShipDestroyed:
                var shipDestroyedPayload = ExtractPayload<AttackPayload>(envelope.Data)!;
                context.CalculateScore(shipDestroyedPayload);
                break;

            default:
                Console.WriteLine("Something went wrong. Player id {0}", this.ID);
                Sessions.SendTo("Something went wrong", this.ID);
                break;
        }
    }

    protected override void OnOpen()
    {
        PlayersManagerInstance.GetInstance().AddPlayer(this);
        Sessions.Broadcast(GetPlayersList());
    }

    private static Context GetOrCreateContext(string? gameId, WebSocketSessionManager session)
    {
        if (gameId != null && _gameContexts.TryGetValue(gameId, out var context)) return context;
        var newGameId = IdGenerator.GenerateId();
        context = new Context(newGameId, session);
        _gameContexts[newGameId] = context;

        return context;
    }

    private static string GetPlayersList()
    {
        return PlayerSerializer.SerializePlayers(PlayersManagerInstance.GetInstance().GetPlayersList());
    }

    private bool ValidateEnvelopeAndSendErrorIfNeeded(WsEnvelope? envelope)
    {
        if (envelope == null) {
            Sessions.SendTo("Invalid message", this.ID);
            return false;
        }

        if (!MessageValidator.ValidatePayload(envelope)) {
            Sessions.SendTo("Invalid message format", this.ID);
            return false;
        }

        return true;
    }

    private T? ExtractPayload<T>(object? data) where T : class
    {
        try {
            return ((JObject)data!)?.ToObject<T>();
        }
        catch (Exception ex) {
            Console.WriteLine($"Failed to extract payload {typeof(T).Name}: {ex.Message}");
            Sessions.SendTo("Invalid payload format", this.ID);
            return null;
        }
    }
}