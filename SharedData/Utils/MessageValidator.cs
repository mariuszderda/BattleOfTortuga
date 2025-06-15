using System;

namespace SharedData.Utils
{
    public static class MessageValidator
    {
        public static bool ValidatePayload(WsEnvelope envelope)
        {
            if (envelope == null) {
                return false;
            }

            if (!ActionConfig.IsValidActionType(envelope.ActionType)) {
                return false;
            }

            bool shouldHavePayload = ActionConfig.RequiresPayload(envelope.ActionType);
            bool hasPayload = envelope.Data != null;

            if (!shouldHavePayload && hasPayload) {
                return false;
            }

            if (shouldHavePayload && !hasPayload) {
                return false;
            }

            return true;
        }

        public static string GetValidationError(WsEnvelope envelope)
        {
            if (envelope == null)
                return "Envelope is null";

            if (!ActionConfig.IsValidActionType(envelope.ActionType))
                return $"Unknown ActionType: {envelope.ActionType}";

            bool shouldHavePayload = ActionConfig.RequiresPayload(envelope.ActionType);
            bool hasPayload = envelope.Data != null;

            if (!shouldHavePayload && hasPayload)
                return $"Action {envelope.ActionType} should not have payload";

            if (shouldHavePayload && !hasPayload)
                return $"Action {envelope.ActionType} requires payload";

            return null; // Brak błędu
        }
    }
}