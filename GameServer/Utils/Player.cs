using SharedData;

namespace GameServer.Utils
{
    public class Player
    {
        public string Id { get; set; }
        public ShipColorType ShipColorType { get; set; }

        public int PlayerScore { get; set; } = 0;

        public Player(string id, ShipColorType shipColor)
        {
            Id = id;
            ShipColorType = shipColor;
        }
    }
}