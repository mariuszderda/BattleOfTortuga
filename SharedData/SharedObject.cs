using Newtonsoft.Json;

namespace SharedData
{
    public class SharedObject
    {
        [JsonProperty(Required = Required.Always)]
        public ActionType ActionType { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Player1Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Player2Id { get; set; }

        [JsonProperty(Required = Required.Default)]
        public string? GameId { get; set; } = "";

        [JsonProperty(Required = Required.Default)]
        public Ship? Ship { get; set; }

        [JsonProperty(Required = Required.Default)]
        public int PositionX { get; set; }

        [JsonProperty(Required = Required.Default)]
        public int PositionY { get; set; }

        public SharedObject(ActionType actionType,
            string player1Id,
            string player2Id)
        {
            ActionType = actionType;
            Player1Id = player1Id;
            Player2Id = player2Id;
        }

        public SharedObject(
            ActionType actionType,
            string player1Id,
            string player2Id,
            string? gameId = null,
            Ship? ship = null,
            int positionX = 0,
            int positionY = 0)
        {
            ActionType = actionType;
            Player1Id = player1Id;
            Player2Id = player2Id;
            GameId = gameId ?? "";
            Ship = ship;
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}