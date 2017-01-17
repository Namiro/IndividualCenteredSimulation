using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using MultiAgentSystem.WatorSystem.ViewModels;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.WatorSystem.Models
{
    internal class Shark : Agent
    {
        #region Properties

        public int Years { get; private set; } = 0;
        public StateEnum State { get; private set; }
        public override Color Color
        {
            get
            {
                if (Years <= 1)
                    return Color.Pink;
                else
                    return Color.Red;
            }
        }
        public WatorEnvironment WatorEnvironment { get; set; }
        public Coordinate OldCoordinate { get; private set; }
        public int GestationPeriod { get; private set; } = 10;
        private int Period = 1;
        public int SurvivalDuration { get; private set; } = 7;
        private int RemainingDuration = 4;

        #endregion

        #region Construtors

        public Shark()
        {
            Texture = XNAWatorGrid.ContentManager.Load<Texture2D>("circle");
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public override void Decide()
        {
            CheckArround();

            switch (DecideAction())
            {
                case ActionEnum.Move:
                    ActionMove();
                    break;
                case ActionEnum.Nothing:
                    ActionNothing();
                    break;
            }

            Period = (Period + 1) % GestationPeriod;
            Years++;
            RemainingDuration--;

        }

        protected override DirectionEnum DecideDirection()
        {
            List<DirectionEnum> possibleDirectionsForFish = new List<DirectionEnum>();
            List<DirectionEnum> possibleDirections = new List<DirectionEnum>();
            foreach (var elem in Neighborhood)
            {
                // Si cellule pas null et est de type Empty, c'est que on est pas a une frontière et que la case n'est pas occupée.
                if (elem.Value != null && elem.Value is Fish)
                    possibleDirectionsForFish.Add(elem.Key);
                else if (elem.Value != null && elem.Value is Empty)
                    possibleDirections.Add(elem.Key);
            }
            
            // Mélange les possibilités.
            if (possibleDirectionsForFish.Count > 0)
                return CurrentDirection = possibleDirectionsForFish[Random.Next(possibleDirectionsForFish.Count)];
            else if(possibleDirections.Count > 0)
                return CurrentDirection = possibleDirections[Random.Next(possibleDirections.Count)];

            return DirectionEnum.NoOne;
        }

        /// <summary>
        /// In this methode, implement the decision of action that need to take the agent.
        /// </summary>
        private ActionEnum DecideAction()
        {
            int actionChoice = App.Random.Next(ActionsNumber);
            switch (actionChoice)
            {
                case 0:
                    return ActionEnum.Move;
                case 1:
                    return ActionEnum.Nothing;
                default:
                    Logger.WriteLog("actionChoice selected doesn't exist or is not treated so agent decide to do nothing", LogLevelL4N.WARN);
                    return ActionEnum.Nothing;
            }
        }

        /// <summary>
        /// To move the agent
        /// </summary>
        protected override void ActionMove()
        {
            // Save this old coordinate
            OldCoordinate = Coordinate;
            // Choose the direction
            DirectionEnum direction = DecideDirection();
            // Free the current position
            Grid.Free(Coordinate);
            
            if(RemainingDuration <= 0)
            {
                ActionDie();
                return;
            }

            Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);

            // If a fish, a shark eats it
            Cell cell = Grid.Get(Coordinate);
            if(cell is Fish)
            {
                Fish fish = (Fish) cell;
                ActionEat(fish);
            }

            // Occupy the new position
            Grid.Occupy(this);

            // If agent moves and conditions are okay then it can reproduce
            if ((!Coordinate.Equals(OldCoordinate)) && Period == (GestationPeriod - 1))
            {
                ActionReproduction();
            }
        }

        protected override void ActionNothing()
        {

        }

        /// <summary>
        /// To reproduce a new shark agent
        /// </summary>
        private void ActionReproduction()
        {
            Shark Shark = new Shark();
            Shark.Coordinate = OldCoordinate;
            Shark.Grid = Grid;
            Shark.WatorEnvironment = WatorEnvironment;
            Grid.Occupy(Shark);
            WatorEnvironment.NewAgents.Add(Shark);
            //Console.WriteLine("Agent,Shark,Birth;");
        }

        /// <summary>
        /// To eat a fish agent
        /// </summary>
        private void ActionEat(Fish fish)
        {
            Grid.Free(Coordinate);
            WatorEnvironment.DeadAgents.Add(fish);
            RemainingDuration = SurvivalDuration;
            //Console.WriteLine("Agent,Fish,Death;");
        }

        /// <summary>
        /// To die
        /// </summary>
        private void ActionDie()
        {
            WatorEnvironment.DeadAgents.Add(this);
            //Console.WriteLine("Agent,Shark,Death;");
        }


        #endregion

        private enum ActionEnum
        {
            Nothing,
            Move
        }
    }
}
