namespace GameServer.Utils
{
    public class Player
    {
        public string Id { get; set; }
        public string? ShipColor { get; set; }

        public Player(string id)
        {
            Id = id;
        }
    }
}