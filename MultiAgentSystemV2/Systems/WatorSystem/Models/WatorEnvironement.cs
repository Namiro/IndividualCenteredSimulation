using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using System;
using System.Collections.Generic;
using System.IO;

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

            this.Initialize(Agents);

            //Test de la redirection
            FileStream fs = new FileStream("Wator.csv", FileMode.Create);
            TextWriter tmp = Console.Out;
            StreamWriter sw = new StreamWriter(fs);
            Console.SetOut(sw);
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

        private void CountDifferentAgents()
        {
            int sharks = 0;
            int fishes = 0;
            foreach (Agent agent in Agents)
            {
                if (agent is Shark)
                    sharks++;
                else if (agent is Fish)
                    fishes++;
            }
            Console.WriteLine("Tick," + sharks + "," + fishes + ";");
        }

        public override void Run()
        {
            base.Run();
            RefreshAgents();
            CountDifferentAgents();
        }

        #endregion
    }

}
