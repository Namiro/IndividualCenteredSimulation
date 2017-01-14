using MultiAgentSystem.Cores.Constants;

namespace MultiAgentSystem.Cores.Helpers.Grids
{
    public class Grid
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }
        public Cell[,] Grid2D { get; private set; }
        public bool IsToric { get; set; }

        public Grid()
        {
        }

        public Grid(int x, int y, bool isToric)
        {
            XSize = x;
            YSize = y;
            IsToric = isToric;

            Grid2D = new Cell[XSize, YSize];
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

        public void Occupy(Cell obj)
        {
            if (!(Grid2D[obj.Coordinate.X, obj.Coordinate.Y] is Empty))
                throw new System.Exception("There is already an object to these coordinates in the grid. - " + obj.Coordinate);

            Grid2D[obj.Coordinate.X, obj.Coordinate.Y] = obj;
        }

        public Cell Get(Coordinate coordinate)
        {
            return Get(coordinate.X, coordinate.Y);
        }

        public Cell Get(int X, int Y)
        {
            if (X < 0 || X > XSize - 1 || Y < 0 || Y > YSize - 1)
                return null;

            return Grid2D[X, Y];
        }

        public void Free(Coordinate coordinate)
        {
            Grid2D[coordinate.X, coordinate.Y] = new Empty();
        }

        public Grid Clone()
        {
            Grid grid = new Grid();
            grid.YSize = this.YSize;
            grid.XSize = this.XSize;
            grid.Grid2D = (Cell[,])this.Grid2D.Clone();

            return grid;
        }

        public Coordinate DirectionToCoordinate(DirectionEnum direction, Coordinate fromPosition)
        {
            Coordinate coordinate = new Coordinate(fromPosition.X, fromPosition.Y);
            switch (direction)
            {
                case DirectionEnum.TopLeft:
                    coordinate.X--;
                    coordinate.Y--;
                    break;
                case DirectionEnum.Top:
                    coordinate.Y--;
                    break;
                case DirectionEnum.BottomLeft:
                    coordinate.X++;
                    coordinate.Y--;
                    break;
                case DirectionEnum.Left:
                    coordinate.X--;
                    break;
                case DirectionEnum.BottomRight:
                    coordinate.X++;
                    coordinate.Y++;
                    break;
                case DirectionEnum.Bottom:
                    coordinate.Y++;
                    break;
                case DirectionEnum.TopRight:
                    coordinate.X--;
                    coordinate.Y++;
                    break;
                case DirectionEnum.Right:
                    coordinate.X++;
                    break;
                case DirectionEnum.NoOne:
                    break;
                default:
                    Logger.WriteLog("Unknown direction : " + direction.ToString(), LogLevelL4N.ERROR);
                    break;
            }

            if (this.IsToric)
                coordinate = RectifyCoordonateToBeToricGrid(coordinate);

            return coordinate;
        }


        private static Coordinate RectifyCoordonateToBeToricGrid(Coordinate coordinate)
        {
            Coordinate coord = new Coordinate(coordinate.X, coordinate.Y);

            if (coordinate.X <= -1)
            {
                coord.X = App.GridSizeX - 1;
            }
            else if (coordinate.X >= App.GridSizeX)
            {
                coord.X = 0;
            }
            else if (coordinate.Y <= -1)
            {
                coord.Y = App.GridSizeY - 1;
            }
            else if (coordinate.Y >= App.GridSizeY)
            {
                coord.Y = 0;
            }

            return coord;
        }
    }
}
