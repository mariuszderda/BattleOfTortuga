namespace SharedData.Payloads
{
    [Serializable]
    public class EndGamePayload
    {
        private string _winnerPlayerId;

        public string WinnerPlayerId
        {
            get { return _winnerPlayerId; }
            set { _winnerPlayerId = value; }
        }

        public EndGamePayload()
        { }

        public EndGamePayload(string winnerPlayerId)
        {
            _winnerPlayerId = winnerPlayerId;
        }
    }
}