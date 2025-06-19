using System;

namespace SharedData.Payloads
{
    [Serializable]
    public class PlayerMovePayload
    {
        private Ship _newPosition;

        public Ship NewPosition
        {
            get { return _newPosition; }
            set { _newPosition = value; }
        }

        public PlayerMovePayload(Ship newPosition)
        {
            _newPosition = newPosition;
        }
    }
}