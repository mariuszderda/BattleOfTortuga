namespace SharedData.Utils
{
    public static class EnvelopeFactory
    {
        public static WsEnvelope CreateNoPayload(ActionType type, string gameId, string p1, string p2)
        {
            if (ActionConfig.RequiresPayload(type))
                throw new ArgumentException($"Action {type} requires payload");

            return new WsEnvelope {
                ActionType = type,
                GameId = gameId,
                Player1Id = p1,
                Player2Id = p2,
                Data = null
            };
        }

        public static WsEnvelope CreateWithPayload<T>(ActionType type, string gameId,
            string p1, string p2, T payload)
        {
            if (!ActionConfig.RequiresPayload(type))
                throw new ArgumentException($"Action {type} should not have payload");

            return new WsEnvelope {
                ActionType = type,
                GameId = gameId,
                Player1Id = p1,
                Player2Id = p2,
                Data = payload
            };
        }
    }
}