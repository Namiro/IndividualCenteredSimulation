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
        int tick = 1;
        StreamWriter sw;

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
            sw = new StreamWriter(fs);
            Console.SetOut(sw);
            Console.WriteLine("Tick_Num,Nb_Sharks,Nb_New_Sharks,Nb_Dead_Sharks,Older_Shark,Nb_Fishes,Nb_New_Fishes,Nb_Dead_Fishes,Older_Fish;");
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

        private int[] CountDifferentAgents(List<IAgent> agents)
        {
            int[] sharksFishes = new int[2];

            foreach (Agent agent in agents)
            {
                if (agent is Shark)
                    sharksFishes[0]++;
                else if (agent is Fish)
                    sharksFishes[1]++;
            }

            return sharksFishes;
        }

        private int[,] CountDifferentYears(List<IAgent> agents)
        {
            int[,] years = new int[2,101];
            foreach (Agent agent in agents)
            {
                if (agent is Shark)
                {
                    Shark shark = (Shark)agent;
                    years[0, shark.Years]++;
                }    
                else if (agent is Fish)
                {
                    Fish fish = (Fish)agent;
                    years[1, fish.Years]++;
                }
            }

            return years;
        }

        private int[] GetOlderAgents()
        {
            int[] olderAgents = new int[2];
            olderAgents[0] = 0;
            olderAgents[1] = 0;
            int[,] years = CountDifferentYears(Agents);
            for (int a = 0; a < olderAgents.GetLength(0); a++)
            {
                for (int i = years.GetLength(1) - 1; i > -1; i--)
                {
                    if (years[a, i] > 0)
                    {
                        olderAgents[a] = i;
                        break;
                    }
                }
            }

            return olderAgents;
        }

        private void Log()
        {
            int[] nbSharksFishes;
            int[] olderSharksFishes;
            int nbSharks = 0, nbNewSharks = 0, nbDeadSharks = 0, nbFishes = 0, nbNewFishes = 0, nbDeadFishes = 0;
            int olderShark = 0, olderFish = 0;

            nbSharksFishes = CountDifferentAgents(NewAgents);
            nbNewSharks = nbSharksFishes[0]; nbNewFishes = nbSharksFishes[1];
            nbSharksFishes = CountDifferentAgents(DeadAgents);
            nbDeadSharks = nbSharksFishes[0]; nbDeadFishes = nbSharksFishes[1];

            RefreshAgents();

            nbSharksFishes = CountDifferentAgents(Agents);
            nbSharks = nbSharksFishes[0]; nbFishes = nbSharksFishes[1];

            olderSharksFishes = GetOlderAgents();
            olderShark = olderSharksFishes[0]; olderFish = olderSharksFishes[1];

            Console.WriteLine(tick + "," + nbSharks + "," + nbNewSharks + "," + nbDeadSharks + "," + olderShark + "," + nbFishes + "," + nbNewFishes + "," + nbDeadFishes + "," + olderFish + ";");

            //Au bout de N tick, on arrête le programme
            if (tick == 100)
            {
                sw.Close();
                System.Environment.Exit(0);
            }
            tick++;
        }

        public override void Run()
        {
            base.Run();
            if(App.IsTraced)
                Log();
            else
                RefreshAgents();
        }

        #endregion
    }

}
