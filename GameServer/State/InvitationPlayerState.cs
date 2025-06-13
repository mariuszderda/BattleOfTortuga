using Newtonsoft.Json;
using SharedData;
using WebSocketSharp.Server;

namespace GameServer.State
{
    public class InvitationPlayerState(Context context) : State(context)
    {
        public override void SendInvitation(string playerId, string invitePlayerId)
        {
            var invitation = new SharedObject(ActionType.InvitePlayer, playerId, invitePlayerId, Context.GameId);
            var serializeInvitation = JsonConvert.SerializeObject(invitation);
            Context.session.SendTo(serializeInvitation, invitePlayerId);
            Context.SetState(new AcceptInviteState(Context));
        }
    }
}