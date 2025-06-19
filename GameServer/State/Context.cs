using GameServer.Utils;
using SharedData;
using SharedData.Payloads;
using WebSocketSharp.Server;

namespace GameServer.State;

public class Context
{
    private State _currentState;

    public string GameId { get; set; }

    public Player? Player1 { get; set; }

    public Player? Player2 { get; set; }

    public readonly WebSocketSessionManager session;
    public List<Ship>? GameBoard { get; set; }

    public Context(string gameId, WebSocketSessionManager session)
    {
        GameId = gameId;
        this.session = session;
        _currentState = new InvitationPlayerState(this);
    }

    public void SetState(State newState)
    {
        _currentState = newState ?? throw new ArgumentNullException(nameof(newState));
    }

    public void SendInvitation(string playerId, string invitePlayerId
        ) => _currentState.SendInvitation(playerId, invitePlayerId);

    public void CreateNewGame(string player1, string player2) =>
        _currentState.CreateNewGame(player1, player2);

    public void CalculateScore(AttackPayload shipDestroyedPayload) =>
        _currentState.CalculateScore(shipDestroyedPayload);

    public void ShipMoving(Ship ship) =>
        _currentState.ShipMoving(ship);

    public void GetShip(int positionX, int positionY) =>
        _currentState.GetShip(positionX, positionY);

    public void EndGame() => _currentState.EndGame();
}