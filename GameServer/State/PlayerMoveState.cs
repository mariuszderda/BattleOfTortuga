using Newtonsoft.Json;
using SharedData;
using SharedData.Utils;

namespace GameServer.State
{
    public class PlayerMoveState(Context context) : State(context)
    {
        public override void GetShip(int positionX, int positionY)
        {
            try {
                var ship = Context.GameBoard!.First(s => s.PositionX == positionX && s.PositionY == positionY);
                var envelope = EnvelopeFactory.CreateWithPayload(ActionType.GetShip, Context.GameId, Context.Player1!.Id,
                    Context.Player2!.Id, ship);
                var message = JsonConvert.SerializeObject(envelope);
                Context.session.SendTo(message, Context.Player1.Id);
                Context.session.SendTo(message, Context.Player2.Id);
            }
            catch (Exception e) {
                // Todo: send error message to client.
                Console.WriteLine(e);
                throw;
            }
        }
    }
}