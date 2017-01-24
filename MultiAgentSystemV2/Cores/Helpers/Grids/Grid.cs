using MultiAgentSystem.Cores.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiAgentSystem.Cores.Helpers.Grids
{
    public class Grid
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }
        public Cell[,] Grid2D { get; private set; }
        public bool IsToric { get; set; }
        private List<int> GridCellsNumber { get; set; } = new List<int>();

        public Grid()
        {
        }

        public Grid(int x, int y, bool isToric)
        {
            XSize = x;
            YSize = y;
            IsToric = isToric;

            Grid2D = new Cell[XSize, YSize];

            for (int i = 0; i < Grid2D.GetLength(0); i++)
            {
                for (int j = 0; j < Grid2D.GetLength(1); j++)
                {
                    Grid2D[i, j] = new Empty(new Coordinate(i, j));
                }
            }

            for (int i = 0; i < App.GridSizeX * App.GridSizeY; i++)
                GridCellsNumber.Add(i);
        }

        public Coordinate CellNumberToCoordinate(int cellNumber)
        {
            int x = cellNumber % App.GridSizeX;
            int y = (cellNumber - x) / App.GridSizeX;

            return new Coordinate(x, y);
        }

        public int CoordinateToCellNumber(Coordinate coordinate)
        {
            return ((coordinate.Y * YSize) + coordinate.X);
        }

        public Cell Occupy(Cell obj)
        {
            Cell previousObj = Grid2D[obj.Coordinate.X, obj.Coordinate.Y];
            Grid2D[obj.Coordinate.X, obj.Coordinate.Y] = obj;
            GridCellsNumber.Remove(this.CoordinateToCellNumber(obj.Coordinate));

            return previousObj;
        }

        public bool IsFree(Coordinate coordinate)
        {
            if (Grid2D[coordinate.X, coordinate.Y] is Empty)
                return true;
            else
                return false;
        }

        public Cell Get(Coordinate coordinate)
        {
            return Get(coordinate.X, coordinate.Y);
        }

        public Coordinate GetRandomFreeCoordinate()
        {
            int randomNumber = App.Random.Next(0, GridCellsNumber.Count);
            int cellNumber = GridCellsNumber[randomNumber];
            GridCellsNumber.Remove(cellNumber);

            return this.CellNumberToCoordinate(cellNumber);
        }

        public Cell Get(int X, int Y)
        {
            if (X < 0 || X > XSize - 1 || Y < 0 || Y > YSize - 1)
                return null;

            return Grid2D[X, Y];
        }

        public void Free(Coordinate coordinate)
        {
            Grid2D[coordinate.X, coordinate.Y] = new Empty(coordinate);
            GridCellsNumber.Add(this.CoordinateToCellNumber(coordinate));
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
                case DirectionEnum.UpLeft:
                    coordinate.X--;
                    coordinate.Y--;
                    break;
                case DirectionEnum.Up:
                    coordinate.Y--;
                    break;
                case DirectionEnum.UpRight:
                    coordinate.X++;
                    coordinate.Y--;
                    break;
                case DirectionEnum.Left:
                    coordinate.X--;
                    break;
                case DirectionEnum.DownRight:
                    coordinate.X++;
                    coordinate.Y++;
                    break;
                case DirectionEnum.Down:
                    coordinate.Y++;
                    break;
                case DirectionEnum.DownLeft:
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

        public void CalculateDijkstra(Coordinate startCoordionate)
        {
            List<Cell> allCell = new List<Cell>();

            List<DirectionEnum> availableDirections = Enum.GetValues(typeof(DirectionEnum)).Cast<DirectionEnum>().ToList();
            if (!App.IsMoore)
            {
                availableDirections.Remove(DirectionEnum.DownLeft);
                availableDirections.Remove(DirectionEnum.DownRight);
                availableDirections.Remove(DirectionEnum.UpLeft);
                availableDirections.Remove(DirectionEnum.UpRight);
            }
            availableDirections.Remove(DirectionEnum.NoOne);

            try
            {


                // Reset
                foreach (Cell cell in Grid2D)
                    cell.DijkstraValue = -1;

                // TODO
                int distance = 1;
                List<Cell> listPos = new List<Cell>();
                listPos.Add(Get(startCoordionate));

                for (int i = 0; i < listPos.Count; i++)
                {
                    listPos = DrijkstraTurn(listPos, distance, availableDirections);
                    distance++;
                    i = 0;
                }

                Cell.DijkstraMaxValue = distance;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.ToString());
            }
        }


        private List<Cell> DrijkstraTurn(List<Cell> listPos, int distance, List<DirectionEnum> availableDirections)
        {
            List<Cell> nextListPos = new List<Cell>();

            for (int i = 0; i < listPos.Count; i++)
            {
                Cell pos = listPos[i];
                List<Cell> around = new List<Cell>();
                foreach (DirectionEnum direction in availableDirections)
                {
                    around.Add(Get(DirectionToCoordinate(direction, pos.Coordinate)));
                }


                for (int j = 0; j < around.Count; j++)
                {
                    Cell position = around[j];
                    if (position is Empty && position.DijkstraValue < 0)
                    {
                        position.DijkstraValue = distance;
                        nextListPos.Add(position);
                    }
                }
            }

            return nextListPos;
        }
    }
}
