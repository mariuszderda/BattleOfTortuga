using SharedData;
using WebSocketSharp.Server;

namespace GameServer.State;

public abstract class State(Context context)
{
    protected Context Context { get; set; } = context;

    public virtual void SendInvitation(string playerId, string invitePlayerId) =>
        Invalid();

    public virtual void CreateNewGame(string player1, string player2) => Invalid();

    public virtual void CalculateScore(Ship ship) => Invalid();

    public virtual void EndGame() => Invalid();

    public virtual void GetShip(int positionX, int positionY) => Invalid();

    private static void Invalid()
    {
        throw new InvalidOperationException("Action not allowed in current state.");
    }
}