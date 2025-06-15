using Newtonsoft.Json;
using SharedData;
using SharedData.Payloads;
using SharedData.Utils;

namespace GameServer.State
{
    public class InvitationPlayerState(Context context) : State(context)
    {
        public override void SendInvitation(string playerId, string invitePlayerId)
        {
            var payload = new InvitationPayload(invitePlayerId);
            var envelope = EnvelopeFactory.CreateWithPayload(ActionType.InvitePlayer, Context.GameId, playerId, invitePlayerId, payload);
            var serializeInvitation = JsonConvert.SerializeObject(envelope);
            Context.session.SendTo(serializeInvitation, invitePlayerId);
            Context.SetState(new AcceptInviteState(Context));
        }
    }
}