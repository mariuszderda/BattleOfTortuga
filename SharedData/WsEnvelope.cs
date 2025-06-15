using Newtonsoft.Json;
using System;

namespace SharedData
{
    [Serializable]
    public class WsEnvelope
    {
        private ActionType _actionType;
        private string _gameId = "";
        private string _player1Id = "";
        private string _player2Id = "";
        private int? _sequence;
        private object _data;

        [JsonProperty("type", Required = Required.Always)]
        public ActionType ActionType
        {
            get { return _actionType; }
            set { _actionType = value; }
        }

        [JsonProperty("gameId", Required = Required.Default)]
        public string? GameId
        {
            get { return _gameId; }
            set { _gameId = value ?? ""; }
        }

        [JsonProperty("p1", Required = Required.Always)]
        public string Player1Id
        {
            get { return _player1Id; }
            set { _player1Id = value; }
        }

        [JsonProperty("p2", Required = Required.Always)]
        public string Player2Id
        {
            get { return _player2Id; }
            set { _player2Id = value; }
        }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}