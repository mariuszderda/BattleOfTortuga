using Newtonsoft.Json;
using GameServer.Utils;
using SharedData;

namespace GameServer.State;

public class AcceptInviteState(Context context) : State(context)
{
    public override void CreateNewGame(string player1, string player2)
    {
        Context.GameBoard = GameBoardCreator.CreateShipsWithRandomPositions();
        Context.Player1 = new Player(player1);
        Context.Player2 = new Player(player2);

        var newGame =
            JsonConvert.SerializeObject(new SharedObject(ActionType.GameReady, Context.Player1.Id, Context.Player2.Id, Context.GameId));
        Context.session.SendTo(newGame, Context.Player1.Id);
        Context.session.SendTo(newGame, Context.Player2.Id);
        Context.SetState(new PlayerMoveState(Context));

        foreach (var ship in Context.GameBoard) {
            Console.WriteLine(
                $"Color: {ship.ShipColor}, Power: {ship.Points}, Position: ({ship.PositionX},{ship.PositionY}), Rotation: {ship.Rotate}°");
        }
    }
}