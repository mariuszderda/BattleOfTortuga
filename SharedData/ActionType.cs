namespace SharedData;

public enum ActionType
{
    InvitePlayer,
    AcceptGame,
    GameReady,
    GetShip,
    PlayerMove,
    Attack,
    ShipDestroyed,
    Score,
    EndTurn,
    EndGame,
    Error = 99,
}