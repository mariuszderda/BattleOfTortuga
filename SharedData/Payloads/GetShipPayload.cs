using System;

namespace SharedData.Payloads
{
    [Serializable]
    public class GetShipPayload
    {
        private int _x;
        private int _y;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public GetShipPayload()
        { }

        public GetShipPayload(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}