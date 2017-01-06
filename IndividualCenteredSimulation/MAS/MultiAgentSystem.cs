using Helpers.Services;
using IndividualCenteredSimulation.Agents;
using IndividualCenteredSimulation.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace IndividualCenteredSimulation.MAS
{
    internal class MultiAgentSystem : Service
    {
        #region Properties

        public Grid Grid { get; set; }

        private List<Agent> Agents { get; set; }
        private List<int> GridCellsNumber { get; set; }

        #endregion

        #region Construtors

        public MultiAgentSystem()
        {
            Random random = new Random();

            Grid = new Grid(App.GridSizeX, App.GridSizeY);

            Agents = new List<Agent>();

            GridCellsNumber = new List<int>();
            for (int i = 0; i < App.GridSizeX * App.GridSizeY; i++)
                GridCellsNumber.Add(i);

            int randomNumber;
            for (int i = 0; i < App.AgentsNumber; i++)
            {
                randomNumber = random.Next(0, GridCellsNumber.Count);
                int cellNumber = GridCellsNumber[randomNumber];
                GridCellsNumber.Remove(cellNumber);

                Color randomColor = Color.FromArgb(random.Next(200), random.Next(200), random.Next(200));
                Coordinate coordinate = Grid.CellNumberToXYCoordinate(cellNumber);
                Grid.Occupy(coordinate, new Agent(coordinate, GraphicHelper.CastColor(randomColor), Grid));
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
                    Logger.WriteLog("Unknown SchedulingStrategy : " + App.SchedulingStrategy.ToString(), LogLevelL4N.FATAL);
                    Environment.Exit(0);
                    break;
            }

            RaisePropertyChanged(nameof(MultiAgentSystem.Grid));
        }

        private void RunRandomly()
        {
            //TODO Implement Radom strategy
            // What is expected for randomly ?
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
            Helper.Shuffle(Agents);

            foreach (Agent agent in Agents)
            {
                agent.Decide();
            }
        }
        #endregion
    }
}
