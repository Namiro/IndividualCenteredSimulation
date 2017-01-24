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

        public int Years { get; private set; } = 10;
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
        public int GestationPeriod { get; private set; } = 8;
        private int Period = 1;
        public int SurvivalDuration { get; private set; } = 4;
        private int RemainingDuration = 4;

        #endregion

        #region Construtors

        public Shark()
        {
            Texture = XNAWatorGrid.ContentManager.Load<Texture2D>("circle");
        }

        public Shark(int Years)
        {
            this.Years = Years;
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

            // TODO Implémenter toutes les actions possibles

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
            // TODO Modifier pour correspondre à l'énoncée

            List<DirectionEnum> possibleDirectionsForFish = new List<DirectionEnum>();
            List<DirectionEnum> possibleDirections = new List<DirectionEnum>();
            foreach (var elem in Neighbors)
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
            else if (possibleDirections.Count > 0)
                return CurrentDirection = possibleDirections[Random.Next(possibleDirections.Count)];

            return DirectionEnum.NoOne;
        }

        /// <summary>
        /// In this methode, implement the decision of action that need to take the agent.
        /// </summary>
        private ActionEnum DecideAction()
        {
            // TODO Modifier pour correspondre à l'énoncée

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
            // TODO Modifier pour correspondre à l'énoncée
            Coordinate OldCoordinate = Coordinate;

            // Choose the direction
            DirectionEnum direction = DecideDirection();
            // Free the current position
            Grid.Free(Coordinate);
            if (RemainingDuration <= 0)
            {
                WatorEnvironment.DeadAgents.Add(this);
                return;
            }

            Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);

            // Il s'agit d'un poisson, on le mange et on vit plus longtemps
            Cell cell = Grid.Get(Coordinate);
            if (cell is Fish)
            {
                Grid.Free(Coordinate);
                Fish fish = (Fish)cell;
                WatorEnvironment.DeadAgents.Add(fish);
                RemainingDuration = SurvivalDuration;
            }

            // Occupy the new position
            Grid.Occupy(this);

            //
            if ((!Coordinate.Equals(OldCoordinate)) && Period == (GestationPeriod - 1))
            {
                //Ajouter le nouveau né dans la Grid
                Shark Shark = new Shark(0);
                Shark.Coordinate = OldCoordinate;
                Shark.Grid = Grid;
                Shark.WatorEnvironment = WatorEnvironment;
                Grid.Occupy(Shark);
                WatorEnvironment.NewAgents.Add(Shark);
            }
        }

        protected override void ActionNothing()
        {

        }

        private void ActionReproduction()
        {

        }
        private void ActionEat()
        {

        }



        #endregion

        private enum ActionEnum
        {
            // TODO Ajouter les actions possible pour correspondre à l'énoncé

            Nothing,
            Move,
            Reproduction,
            Eat
        }
    }
}
