namespace IndividualCenteredSimulation.Helpers
{
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x = int.MinValue, int y = int.MinValue)
        {
            X = x;
            Y = y;
        }
    }
}
