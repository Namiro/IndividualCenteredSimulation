using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Helpers.Grids;
using System;

namespace MultiAgentSystem.HunterSystem.Models
{
    internal class HunterEnvironment : Cores.Models.Environment
    {
        #region Properties

        public Avatar Avatar { get; private set; } = new Avatar();

        #endregion

        #region Construtors

        public HunterEnvironment() : base()
        {

        }

        #endregion

        #region Methodes

        public override void Initialize()
        {
            try
            {
                int nbWall = (int)(Grid.XSize * Grid.YSize * (App.WallsPercent / 100f));
                for (int i = 0; i < nbWall; i++)
                {
                    Wall wall = new Wall();
                    wall.Coordinate = Grid.GetRandomFreeCoordinate();
                    Grid.Occupy(wall);
                }


                for (int i = 0; i < App.HuntersNumber; i++)
                    Agents.Add(new Hunter());

                Agents.Add(Avatar);

                base.Initialize();


                // Transform all inaccesible cell as  Wall
                Grid.CalculateDijkstra(Avatar.Coordinate);
                for (int i = 0; i < Grid.Grid2D.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.Grid2D.GetLength(1); j++)
                    {
                        if (Grid.Grid2D[i, j].DijkstraValue == -1 && Grid.Grid2D[i, j] is Empty)
                        {
                            Wall wall = new Wall();
                            wall.Coordinate = Grid.Grid2D[i, j].Coordinate;
                            Grid.Occupy(wall);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex + "");
            }
        }

        #endregion
    }
}
