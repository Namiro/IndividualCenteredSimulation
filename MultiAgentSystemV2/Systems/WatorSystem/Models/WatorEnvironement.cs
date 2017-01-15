using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.WatorSystem.Models
{
    internal class WatorEnvironment : Cores.Models.Environment
    {
        #region Properties

        public List<IAgent> NewAgents { get; set; } = new List<IAgent>();

        #endregion

        #region Construtors

        public WatorEnvironment() : base()
        {
            Console.WriteLine("lol");

            for (int i = 0; i < App.FishsNumber; i++)
            {
                Fish Fish = new Fish();
                Fish.WatorEnvironment = this;
                Agents.Add(Fish);
            }

            for (int i = 0; i < App.SharksNumber; i++)
                Agents.Add(new Shark());

            this.Initialize(Agents);
        }

        #endregion

        #region Methodes

        private void RefreshAgents()
        {
            foreach (Agent agent in NewAgents)
            {
                Agents.Add(agent);
            }
            NewAgents = new List<IAgent>();
        }

        public override void Run()
        {
            Console.WriteLine("c'est le Run");
            Console.WriteLine(NewAgents.Count);
            
            base.Run();
            
            if(NewAgents.Count > 0)
            {
                foreach (Agent agent in NewAgents)
                {
                    Agents.Add(agent);
                }
                NewAgents.Clear();
            }
            

            /*
            Fish f = new Fish();
            f.Coordinate = new Coordinate(0, 0);
            Agents.Add(f);
            */
            Console.WriteLine("Agents.count " + Agents.Count);
        }

        #endregion
    }

}
