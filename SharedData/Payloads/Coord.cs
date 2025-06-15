namespace SharedData.Payloads
{
    [Serializable]
    public class Coord
    {
        private int _x;
        private int _y;

        public Coord()
        { }

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
    }
}