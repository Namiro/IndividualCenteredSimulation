namespace MultiAgentSystem.Helpers
{
    public class Grid
    {
        public int XSize { get; }
        public int YSize { get; }
        private object[,] Grid2D { get; }

        public Grid(int x, int y)
        {
            XSize = x;
            YSize = y;

            Grid2D = new object[XSize, YSize];
            Helper.Populate(Grid2D, new Empty());
        }

        public Coordinate CellNumberToXYCoordinate(int cellNumber)
        {
            int x = cellNumber % App.GridSizeX;
            int y = (cellNumber - x) / App.GridSizeX;

            return new Coordinate(x, y);
        }

        public int XYCoordinateToCellNumber(Coordinate coordinate)
        {
            return ((coordinate.Y * YSize) + coordinate.X);
        }

        public void Occupy(Coordinate coordinate, object obj)
        {
            if (!(Grid2D[coordinate.X, coordinate.Y] is Empty))
                throw new System.Exception("There is already an object to these coordinates in the grid. - " + coordinate);

            Grid2D[coordinate.X, coordinate.Y] = obj;
        }

        public object Get(Coordinate coordinate)
        {
            return Get(coordinate.X, coordinate.Y);
        }

        public object Get(int X, int Y)
        {
            if (X < 0 || X > XSize - 1 || Y < 0 || Y > YSize - 1)
                return null;

            return Grid2D[X, Y];
        }

        public void Free(Coordinate coordinate)
        {
            Grid2D[coordinate.X, coordinate.Y] = new Empty();
        }
    }
}
