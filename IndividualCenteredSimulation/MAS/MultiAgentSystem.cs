using Helpers.Services;
using IndividualCenteredSimulation.Agents;
using IndividualCenteredSimulation.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace IndividualCenteredSimulation.MAS
{
    internal class MultiAgentSystem : Service
    {
        #region Properties

        public Grid Grid { get; set; }

        private List<Agent> Agents { get; set; }
        private List<int> GridCellsNumber { get; set; }
        private Timer TimerTick { get; set; } = new Timer();

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

                Coordinate coordinate = Grid.CellNumberToXYCoordinate(cellNumber);
                Grid.Occupy(coordinate, new Agent(coordinate, GraphicHelper.CastColor(Color.FromArgb(255, random.Next(200), random.Next(200), random.Next(200))), Grid, cellNumber));
                Agents.Add((Agent)Grid.Get(coordinate));
            }

            // Initialize the timers
            TimerTick.Interval = 5000;
            TimerTick.Elapsed += Run;
            TimerTick.Enabled = true;
        }

        #endregion

        #region Methodes

        public void Run(object myObject, EventArgs myEventArgs)
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
