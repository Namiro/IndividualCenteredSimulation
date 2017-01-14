using Helpers.Services;
using MultiAgentSystem.Helpers.Grids;
using MultiAgentSystem.Models.Agents;
using MultiAgentSystemV2;
using System.Collections.Generic;
using System.Timers;

namespace MultiAgentSystem.Environments
{
    internal class Environment : Service
    {
        #region Properties

        public Grid Grid { get; set; }

        public List<Agent> Agents { get; private set; }
        private List<int> GridCellsNumber { get; set; }
        private Timer TimerTick { get; set; } = new Timer();

        #endregion

        #region Construtors

        public Environment()
        {
            Grid = new Grid(App.GridSizeX, App.GridSizeY, App.IsToric);

            Agents = new List<Agent>();

            GridCellsNumber = new List<int>();
            for (int i = 0; i < App.GridSizeX * App.GridSizeY; i++)
                GridCellsNumber.Add(i);

            int randomNumber;
            for (int i = 0; i < App.AgentsNumber; i++)
            {
                randomNumber = App.Random.Next(0, GridCellsNumber.Count);
                int cellNumber = GridCellsNumber[randomNumber];
                GridCellsNumber.Remove(cellNumber);

                Coordinate coordinate = Grid.CellNumberToXYCoordinate(cellNumber);
                Grid.Occupy(coordinate, new Agent(coordinate, Grid));
                Agents.Add((Agent)Grid.Get(coordinate));
            }
        }

        #endregion

        #region Methodes

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
            Agent[] agents = Agents.ToArray();

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
