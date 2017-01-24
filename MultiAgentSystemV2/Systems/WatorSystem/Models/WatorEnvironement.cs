using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Models;
using System.Collections.Generic;

namespace MultiAgentSystem.WatorSystem.Models
{
    internal class WatorEnvironment : Cores.Models.Environment
    {
        #region Properties

        public List<IAgent> NewAgents { get; private set; } = new List<IAgent>();
        public List<IAgent> DeadAgents { get; private set; } = new List<IAgent>();

        #endregion

        #region Construtors

        public WatorEnvironment() : base()
        {
            if (App.GridSizeX * App.GridSizeY < App.FishsNumber + App.SharksNumber)
            {
                Logger.WriteLog("Fish number more Shark number is too big to match with the grid size.", LogLevelL4N.FATAL);
                throw new System.Exception("Fish number more Shark number is too big to match with the grid size.");
            }

            for (int i = 0; i < App.FishsNumber; i++)
            {
                Fish Fish = new Fish();
                Fish.WatorEnvironment = this;
                Agents.Add(Fish);
            }

            for (int i = 0; i < App.SharksNumber; i++)
            {
                Shark Shark = new Shark();
                Shark.WatorEnvironment = this;
                Agents.Add(Shark);
            }

            this.Initialize();
        }

        #endregion

        #region Methodes

        private void RefreshAgents()
        {
            foreach (Agent agent in NewAgents)
            {
                Agents.Add(agent);
            }
            foreach (Agent agent in DeadAgents)
            {
                Agents.Remove(agent);
            }

            NewAgents.Clear();
            DeadAgents.Clear();
        }

        public override void Run()
        {
            base.Run();
            RefreshAgents();
        }

        #endregion
    }

}
