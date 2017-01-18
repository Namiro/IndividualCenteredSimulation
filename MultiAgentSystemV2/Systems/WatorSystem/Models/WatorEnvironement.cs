﻿using MultiAgentSystem.Cores.Helpers;
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

        /// <summary>
        /// The list of the newborn agents (Sharks and Fishes) after each tick.
        /// </summary>
        public static List<IAgent> NewbornAgents { get; private set; } = new List<IAgent>();
        /// <summary>
        /// The list of the dead agents (Sharks and Fishes) after each tick.
        /// </summary>
        public static List<IAgent> DeadAgents { get; private set; } = new List<IAgent>();
        private int tickCount = 1;
        private StreamWriter sw;

        //The statistics in Wator's environment
        public static int SharksNumber { get; set; } = App.SharksNumber;
        public static int NewbornSharksNumber { get; set; } = 0;
        public static int DeadSharksNumber { get; set; } = 0;
        public static int AgeOfOlderShark { get; set; } = 0;
        public static int[] SharkAgePyramid { get; private set; } = new int[1000 +1];

        public static int FishsNumber { get; set; } = App.FishsNumber;
        public static int NewbornFishsNumber { get; set; } = 0;
        public static int DeadFishsNumber { get; set; } = 0;
        public static int AgeOfOlderFish { get; set; } = 0;
        public static int[] FishAgePyramid { get; private set; } = new int[1000 +1];

        #endregion

        #region Construtors

        public WatorEnvironment() : base()
        {
            for (int i = 0; i < App.FishsNumber; i++)
            {
                Agents.Add(new Fish());
            }

            for (int i = 0; i < App.SharksNumber; i++)
            {
                Agents.Add(new Shark());
            }

            this.Initialize(Agents);

            //Redirection to file Wator.csv
            FileStream fs = new FileStream("Wator.csv", FileMode.Create);
            TextWriter tmp = Console.Out;
            sw = new StreamWriter(fs);
            Console.SetOut(sw);
            Console.WriteLine("Tick Count;Sharks Number;Newborn Sharks Number;Dead Sharks Number;Age Of Older Shark;Fishs Number;Newborn Fishs Number;Dead Fishs Number;Age Of Older Fish");
        }

        #endregion

        #region Methodes

        private void RefreshAgents()
        {
            foreach (Agent agent in NewbornAgents)
            {
                Agents.Add(agent);
            }
            foreach (Agent agent in DeadAgents)
            {
                Agents.Remove(agent);
            }

            NewbornAgents.Clear();
            DeadAgents.Clear();

            foreach(Agent agent in Agents)
            {
                if(agent is Fish)
                {
                    Fish fish = (Fish)agent;
                    if (fish.Ages > WatorEnvironment.AgeOfOlderFish)
                        WatorEnvironment.AgeOfOlderFish = fish.Ages;
                }
                else if(agent is Shark)
                {
                    Shark shark = (Shark)agent;
                    if (shark.Ages > WatorEnvironment.AgeOfOlderShark)
                        WatorEnvironment.AgeOfOlderShark = shark.Ages;
                }
            }
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


        public override void Run()
        {
            base.Run();
            
            RefreshAgents();

            if (App.IsTraced)
            {
                if (tickCount == 100)
                {
                    sw.Close();
                    System.Environment.Exit(0);
                }


                Console.WriteLine(
                    tickCount + ";" +
                    WatorEnvironment.SharksNumber + ";" + WatorEnvironment.NewbornSharksNumber + ";" + WatorEnvironment.DeadSharksNumber + ";" + WatorEnvironment.AgeOfOlderShark + ";" +
                    WatorEnvironment.FishsNumber + ";" + WatorEnvironment.NewbornFishsNumber + ";" + WatorEnvironment.DeadFishsNumber + ";" + WatorEnvironment.AgeOfOlderFish
                );

                WatorEnvironment.NewbornSharksNumber = 0;
                WatorEnvironment.DeadSharksNumber = 0;
                WatorEnvironment.AgeOfOlderShark = 0;
                WatorEnvironment.NewbornFishsNumber = 0;
                WatorEnvironment.DeadFishsNumber = 0;
                WatorEnvironment.AgeOfOlderFish = 0;

                tickCount++;
            }                
                
        }

        #endregion
    }

}
