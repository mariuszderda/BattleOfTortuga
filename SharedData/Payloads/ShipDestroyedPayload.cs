using System;
using System.Collections.Generic;

namespace SharedData.Payloads
{
    [Serializable]
    public class ShipDestroyedPayload
    {
        private List<Coord> _cells;
        private string _killerId;

        public List<Coord> Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }

        public string KillerId
        {
            get { return _killerId; }
            set { _killerId = value; }
        }

        public ShipDestroyedPayload()
        {
            _cells = new List<Coord>();
        }

        public ShipDestroyedPayload(List<Coord> cells, string killerId)
        {
            _cells = cells ?? new List<Coord>();
            _killerId = killerId;
        }
    }
}