using GameServer.Utils;
using Newtonsoft.Json;
using SharedData;
using SharedData.Utils;

namespace GameServer.State;

public class AcceptInviteState(Context context) : State(context)
{
    public override void CreateNewGame(string player1, string player2)
    {
        Context.GameBoard = GameBoardCreator.CreateShipsWithRandomPositions();
        Context.Player1 = new Player(player1, ShipColorType.Black);
        Context.Player2 = new Player(player2, ShipColorType.Red);

        var envelope =
            EnvelopeFactory.CreateNoPayload(ActionType.GameReady, Context.GameId, Context.Player1.Id, Context.Player2.Id);
        var envelopeJSON = JsonConvert.SerializeObject(envelope);
        Context.session.SendTo(envelopeJSON, Context.Player1.Id);
        Context.session.SendTo(envelopeJSON, Context.Player2.Id);
        Context.SetState(new PlayerMoveState(Context));

        foreach (var ship in Context.GameBoard) {
            Console.WriteLine(
                $"Color: {ship.ShipColorType}, Power: {ship.Points}, Position: ({ship.PositionX},{ship.PositionY}), Rotation: {ship.Rotate}°");
        }
    }
}