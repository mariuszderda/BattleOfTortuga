using Newtonsoft.Json;
using SharedData;

namespace GameServer.State
{
    public class PlayerMoveState(Context context) : State(context)
    {
        public override void GetShip(int positionX, int positionY)
        {
            try {
                var ship = Context.GameBoard!.First(s => s.PositionX == positionX && s.PositionY == positionY);
                SharedObject response = new(ActionType.GetShip, Context.Player1!.Id, Context.Player2!.Id,
                    Context.GameId, ship);
                var messageData = JsonConvert.SerializeObject(response);
                Context.session.SendTo(messageData, Context.Player1.Id);
                Context.session.SendTo(messageData, Context.Player2.Id);
            }
            catch (Exception e) {
                // Todo: send error message to client.
                Console.WriteLine(e);
                throw;
            }
        }
    }
}