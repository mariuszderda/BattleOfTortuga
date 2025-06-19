using Newtonsoft.Json;
using SharedData;
using SharedData.Payloads;
using SharedData.Utils;

namespace GameServer.State
{
    public class PlayerMoveState(Context context) : State(context)
    {
        public override void GetShip(int positionX, int positionY)
        {
            try {
                var ship = Context.GameBoard!.FirstOrDefault(s => s.PositionX == positionX && s.PositionY == positionY);
                var envelope = EnvelopeFactory.CreateWithPayload(ActionType.GetShip, Context.GameId,
                    Context.Player1!.Id,
                    Context.Player2!.Id, ship);
                BroadcastMessage(envelope);
            }
            catch (Exception e) {
                SendErrorMessage("Nie znaleziono statku");
                Console.WriteLine(e);
            }
        }

        public override void ShipMoving(Ship ship)
        {
            try {
                var index = Context.GameBoard.FindIndex(s =>
                    s.PositionX == ship.PositionX && s.PositionY == ship.PositionY);
                if (index == -1) return;
                Context.GameBoard[index] = ship;
                var envelope = EnvelopeFactory.CreateWithPayload(ActionType.PlayerMove, Context.GameId,
                    Context.Player1!.Id, Context.Player2!.Id, Context.GameBoard[index]);
                BroadcastMessage(envelope);
            }
            catch (Exception e) {
                SendErrorMessage($"Ship don't find in method \"Ship ShipMoving\"");
                Console.WriteLine(e);
            }
        }

        public override void CalculateScore(AttackPayload attackPayload)
        {
            var destroyedShipCoord = attackPayload.DestroyedShipCoord;
            var movingShip = attackPayload.MovingShip;

            if (!TryFindShip(destroyedShipCoord.X, destroyedShipCoord.Y, out Ship destroyedShip)) {
                Console.WriteLine($"Statek na pozycji: x:{destroyedShipCoord.X}, y:{destroyedShipCoord.Y}, nie został znaleziony!");
                SendErrorMessage($"Ship don't find in method \"Ship ShipMoving\"");
                return;
            }

            UpdatePlayerScore(destroyedShip);
            ReplaceShip(destroyedShip, movingShip);

            if (CheckWinner(out string winnerPlayerId)) {
                var payload = new EndGamePayload(winnerPlayerId);
                var envelope = EnvelopeFactory.CreateWithPayload(ActionType.EndGame, Context.GameId, Context.Player1.Id, Context.Player2.Id, payload);
                BroadcastMessage(envelope);
                Context.SetState(new EndGameState(Context));
            }
            else {
                var payload = new ShipDestroyedPayload(destroyedShipCoord, movingShip, Context.Player1.PlayerScore,
                    Context.Player2.PlayerScore);
                var envelope = EnvelopeFactory.CreateWithPayload(ActionType.ShipDestroyed, Context.GameId,
                    Context.Player1!.Id,
                    Context.Player2!.Id, payload);
                BroadcastMessage(envelope);
            }
        }

        private void UpdatePlayerScore(Ship destroyedShip)
        {
            if (destroyedShip.ShipColorType == ShipColorType.Black) {
                Context.Player2!.PlayerScore += destroyedShip.Points;
            }
            else {
                Context.Player1!.PlayerScore += destroyedShip.Points;
            }
        }

        private void BroadcastMessage(WsEnvelope envelope)
        {
            var message = JsonConvert.SerializeObject(envelope);
            Context.session.SendTo(message, Context.Player1!.Id);
            Context.session.SendTo(message, Context.Player2!.Id);
        }

        private bool TryFindShip(int x, int y, out Ship foundShip)
        {
            foundShip = Context.GameBoard.FirstOrDefault(s => s.PositionX == x && s.PositionY == y);
            return foundShip != null;
        }

        private void SendErrorMessage(string message)
        {
            var payload = new ErrorPayload(message);
            var envelope = EnvelopeFactory.CreateWithPayload(ActionType.Error, Context.GameId, Context.Player1.Id,
                Context.Player2.Id, payload);
            BroadcastMessage(envelope);
        }

        private void ReplaceShip(Ship destroyedShip, Ship newShip)
        {
            var index = Context.GameBoard.FindIndex(s =>
                s.PositionX == destroyedShip.PositionX && s.PositionY == destroyedShip.PositionY);
            if (index == -1) return;
            Context.GameBoard.RemoveAt(index);
            Context.GameBoard.Insert(index, newShip);
        }

        private bool CheckWinner(out string winnerPlayerId)
        {
            winnerPlayerId = string.Empty;

            if (Context.Player1!.PlayerScore >= 7) {
                winnerPlayerId = Context.Player1.Id;
                return true;
            }
            else if (Context.Player2!.PlayerScore >= 7) {
                winnerPlayerId = Context.Player2.Id;
                return true;
            }
            return false;
        }
    }
}