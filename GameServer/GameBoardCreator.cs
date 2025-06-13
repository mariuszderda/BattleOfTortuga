using SharedData;

namespace GameServer
{
    public static class GameBoardCreator
    {
        private static readonly Random random = new();

        public static List<Ship> CreateShipsWithRandomPositions()
        {
            var ships = new List<Ship>();

            var powerCounts = new Dictionary<int, int>
            {
                { 1, 2 },
                { 2, 2 },
                { 3, 2 }
            };

            int boardSize = 4;
            int totalShipsPerColor = powerCounts.Values.Sum();

            var availablePositions = new List<(int x, int y)>();
            for (int y = 0; y < boardSize; y++) {
                for (int x = 0; x < boardSize; x++) {
                    bool isCorner = (x == 0 && y == 0) ||
                                    (x == 0 && y == boardSize - 1) ||
                                    (x == boardSize - 1 && y == 0) ||
                                    (x == boardSize - 1 && y == boardSize - 1);

                    if (!isCorner)
                        availablePositions.Add((x, y));
                }
            }

            foreach (ShipColor color in new[] { ShipColor.Black, ShipColor.Red }) {
                var powers = new List<int>();
                foreach (var kvp in powerCounts) {
                    for (int i = 0; i < kvp.Value; i++)
                        powers.Add(kvp.Key);
                }

                powers = [.. powers.OrderBy(_ => random.Next())];

                for (int i = 0; i < totalShipsPerColor; i++) {
                    if (availablePositions.Count == 0)
                        throw new InvalidOperationException("Brak dostępnych pozycji na planszy dla statków.");

                    int posIndex = random.Next(availablePositions.Count);
                    var (x, y) = availablePositions[posIndex];
                    availablePositions.RemoveAt(posIndex);

                    // Losujemy rotację spośród dozwolonych wartości
                    int[] possibleRotations = [0, 90, 180, 270];
                    int rotation = possibleRotations[random.Next(possibleRotations.Length)];

                    ships.Add(new Ship(color, powers[i], x, y, rotation));
                }
            }

            return ships;
        }
    }
}