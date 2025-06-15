using System;
using System.Collections.Generic;

namespace SharedData.Utils
{
    public static class ActionConfig
    {
        private static readonly HashSet<ActionType> NoPayloadActions = new HashSet<ActionType>
        {
            ActionType.AcceptGame,
            ActionType.EndTurn,
            ActionType.EndGame,
            ActionType.GameReady
        };

        private static readonly Dictionary<ActionType, Type> PayloadTypes = new Dictionary<ActionType, Type>
        {
            { ActionType.InvitePlayer, typeof(Payloads.InvitationPayload) },
            { ActionType.GetShip, typeof(Payloads.GetShipPayload) },
            { ActionType.ShipDestroyed, typeof(Payloads.ShipDestroyedPayload) },
            { ActionType.PlayerMove, typeof(Payloads.PlayerMovePayload) }
        };

        public static bool RequiresPayload(ActionType action)
        {
            return !NoPayloadActions.Contains(action);
        }

        public static Type GetPayloadType(ActionType action)
        {
            Type payloadType;
            return PayloadTypes.TryGetValue(action, out payloadType) ? payloadType : null;
        }

        public static bool IsValidActionType(ActionType actionType)
        {
            return Enum.IsDefined(typeof(ActionType), actionType);
        }
    }
}