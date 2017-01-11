using MultiAgentSystem.Helpers;
using MultiAgentSystem.Models.Agents;
using System;
using System.Collections.Generic;
using System.Timers;

namespace MultiAgentSystem.Environments
{
    internal class Environment : Helpers.Services.Service
    {
        #region Properties

        public Grid Grid { get; set; }

        private List<Agent> Agents { get; set; }
        private List<int> GridCellsNumber { get; set; }
        private Timer TimerTick { get; set; } = new Timer();
        private int TickNb { get; set; } = 0;

        #endregion

        #region Construtors

        public Environment()
        {
            Grid = new Grid(App.GridSizeX, App.GridSizeY);

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
                Grid.Occupy(coordinate, new Agent(coordinate, new SharpDX.Color(App.Random.Next(200), App.Random.Next(200), App.Random.Next(200)), Grid, cellNumber));
                Agents.Add((Agent)Grid.Get(coordinate));
            }

            // Initialize the timers
            TimerTick.Interval = App.DelayMilliseconde;
            TimerTick.Elapsed += Run;
            TimerTick.Enabled = true;
        }

        #endregion

        #region Methodes

        public void Run(object myObject, EventArgs myEventArgs)
        {
            App.StartExec = DateTime.Now;
            TickNb++;
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
                    System.Environment.Exit(0);
                    break;
            }

            if (App.IsTracedPerformance)
                Logger.WriteLog("Calcul time : " + DateTime.Now.Subtract(App.StartExec).Milliseconds);

            if (App.TicksNumber != 0 && TickNb >= App.TicksNumber)
                TimerTick.Enabled = false;

            if (!Convert.ToBoolean(TickNb % App.RateRefresh))
                RaisePropertyChanged(nameof(Environment.Grid));

            App.Trace("Tick");
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
            Helper.Shuffle(Agents, App.Random);

            foreach (Agent agent in Agents)
            {
                agent.Decide();
            }
        }
        #endregion
    }
}
