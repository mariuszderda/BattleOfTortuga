using GameServer.State;
using GameServer.Utils;
using Newtonsoft.Json;
using SharedData;
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
        SharedObject? deserializeMessage;

        try {
            deserializeMessage = JsonConvert.DeserializeObject<SharedObject>(message);
        }
        catch (JsonException ex) {
            Console.WriteLine("Deserialize error" + ex.Message);
            Sessions.SendTo("Message format error", this.ID);
            return;
        }

        if (deserializeMessage == null) {
            Console.WriteLine("An empty or invalid message was received from the customer " + this.ID);
            Sessions.SendTo("Invalid message", this.ID);
            return;
        }

        var context = GetOrCreateContext(deserializeMessage.GameId, Sessions);

        switch (deserializeMessage.ActionType) {
            case ActionType.InvitePlayer:
                context.SendInvitation(deserializeMessage.Player1Id, deserializeMessage.Player2Id);
                break;

            case ActionType.AcceptGame:
                context.CreateNewGame(deserializeMessage.Player1Id, deserializeMessage.Player2Id);
                break;

            case ActionType.GetShip:
                context.GetShip(deserializeMessage.PositionX, deserializeMessage.PositionY);
                break;

            case ActionType.PlayerMove:
                // context.CalculateScore();
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
}