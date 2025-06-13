namespace SharedData
{
    public class Ship
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public ShipColor ShipColor { get; set; }
        public int Points { get; set; }
        public float Rotate { get; set; }
        // ships.Add(new Ship(color, powers[i], pos.x, pos.y, rotation));

        public Ship(ShipColor shipColor, int points, int positionX, int positionY, float rotate)
        {
            PositionX = positionX;
            PositionY = positionY;
            ShipColor = shipColor;
            Points = points;
            Rotate = rotate;
        }
    }
}