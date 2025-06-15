using System;

namespace SharedData
{
    [Serializable]
    public class Ship
    {
        private int _positionX;
        private int _positionY;
        private ShipColorType _shipColorType;
        private int _points;
        private float _rotate;

        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public ShipColorType ShipColorType
        {
            get { return _shipColorType; }
            set { _shipColorType = value; }
        }

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public float Rotate
        {
            get { return _rotate; }
            set { _rotate = value; }
        }

        // Bezparametrowy konstruktor dla JSON deserializacji
        public Ship()
        {
        }

        // Konstruktor z parametrami
        public Ship(ShipColorType shipColorType, int points, int positionX, int positionY, float rotate)
        {
            _positionX = positionX;
            _positionY = positionY;
            _shipColorType = shipColorType;
            _points = points;
            _rotate = rotate;
        }
    }
}