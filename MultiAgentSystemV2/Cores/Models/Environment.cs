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

        /// <summary>
        /// Initialize the agents and the grid. So must be call after that the agents has been add to the list of agents (Agents). 
        /// </summary>
        public virtual void Initialize()
        {
            foreach (Agent agent in Agents)
            {
                agent.Coordinate = Grid.GetRandomFreeCoordinate();
                agent.Grid = Grid;
                Grid.Occupy(agent);
            }
        }


        /// <summary>
        /// This function all to run one "tick" for the MAS (Multi Agent System)
        /// </summary>
        public virtual void Run()
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
