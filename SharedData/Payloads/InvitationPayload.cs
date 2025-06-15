using System;

namespace SharedData.Payloads
{
    [Serializable]
    public class InvitationPayload
    {
        private string _inviteeId;

        public string InviteeId
        {
            get { return _inviteeId; }
            set { _inviteeId = value; }
        }

        public InvitationPayload()
        { }

        public InvitationPayload(string inviteeId)
        {
            _inviteeId = inviteeId;
        }
    }
}