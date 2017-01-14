using Helpers.Services;
using MultiAgentSystem.Cores.Helpers.Grids;
using System.Collections.Generic;

namespace MultiAgentSystem.Cores.Models
{
    internal class Environment : Service
    {
        #region Properties

        public Grid Grid { get; set; }
        public List<IAgent> Agents { get; private set; }

        #endregion

        #region Construtors

        public Environment()
        {
            Agents = new List<IAgent>();
            Grid = new Grid(App.GridSizeX, App.GridSizeY, App.IsToric);
        }

        #endregion

        #region Methodes

        public void Initialize(List<IAgent> agents)
        {
            Agents = agents;

            List<int> gridCellsNumber = new List<int>();
            for (int i = 0; i < App.GridSizeX * App.GridSizeY; i++)
                gridCellsNumber.Add(i);

            int randomNumber;
            foreach (Agent agent in Agents)
            {
                randomNumber = App.Random.Next(0, gridCellsNumber.Count);
                int cellNumber = gridCellsNumber[randomNumber];
                gridCellsNumber.Remove(cellNumber);

                agent.Coordinate = Grid.CellNumberToXYCoordinate(cellNumber);
                agent.Grid = Grid;
                Grid.Occupy(agent);
            }
        }

        public void Run()
        {
            switch (App.SchedulingStrategy)
            {
                case Constants.SchedulingStrategyEnum.Fair:
                    RunFairly();
                    break;
                case Constants.SchedulingStrategyEnum.Sequential:
                    RunSequentialy();
                    break;
                case Constants.SchedulingStrategyEnum.Random:
                    RunRandomly();
                    break;
                default:
                    Helpers.Logger.WriteLog("Unknown SchedulingStrategy : " + App.SchedulingStrategy.ToString(), Helpers.LogLevelL4N.FATAL);
                    System.Environment.Exit(0);
                    break;
            }
        }

        private void RunRandomly()
        {
            IAgent[] agents = Agents.ToArray();

            for (int i = 0; i < agents.Length; i++)
            {
                agents[App.Random.Next(agents.Length)].Decide();
            }
        }

        private void RunSequentialy()
        {
            foreach (Agent agent in Agents)
            {
                agent.Decide();
            }
        }

        private void RunFairly()
        {
            Helpers.Helper.Shuffle(Agents, App.Random);

            foreach (Agent agent in Agents)
            {
                agent.Decide();
            }
        }
        #endregion
    }
}
