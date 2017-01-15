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
    internal class Fish : Agent
    {
        #region Properties

        public int Years { get; private set; } = 10;
        public StateEnum State { get; private set; }
        public override Color Color
        {
            get
            {
                if (Years <= 1)
                    return Color.Green;
                else
                    return Color.Blue;
            }
        }
        public int GestationPeriod { get; private set; } = 5;
        private int Period = 1;
        public WatorEnvironment WatorEnvironment { get; set; }

        #endregion

        #region Construtors

        public Fish()
        {
            Texture = XNAWatorGrid.ContentManager.Load<Texture2D>("circle");
        }

        public Fish(int Years)
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
        }

        protected override DirectionEnum DecideDirection()
        {
            // TODO Modifier pour correspondre à l'énoncée

            List<DirectionEnum> possibleDirections = new List<DirectionEnum>();
            foreach (var elem in Neighborhood)
            {
                // Si cellule pas null et est de type Empty, c'est que on est pas a une frontière et que la case n'est pas occupée.
                if (elem.Value != null && elem.Value is Empty)
                    possibleDirections.Add(elem.Key);
            }

            // Si la direction actuel est toujours possible on conitnue dans la même direction.
            if (possibleDirections.Contains(CurrentDirection))
                return CurrentDirection;

            // Mélange les possibilités.
            if (possibleDirections.Count > 0)
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
            // Change the position of the agent on the grid
            Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);
            // Occupy the new position
            Grid.Occupy(this);

            //
            if ((!Coordinate.Equals(OldCoordinate)) && Period == (GestationPeriod -1))
            {
                Console.WriteLine("Reproduction");
                //Ajouter le nouveau né dans la Grid
                Fish Fish = new Fish(0);
                Fish.Coordinate = OldCoordinate;
                Fish.Grid = Grid;
                Fish.WatorEnvironment = WatorEnvironment;
                Grid.Occupy(Fish);
                Console.WriteLine(WatorEnvironment);
                WatorEnvironment.NewAgents.Add(Fish);
                Console.WriteLine("NewAgents.count " + WatorEnvironment.NewAgents.Count);
                
            }
        }


        protected override void ActionNothing()
        {

        }

        private void ActionReproduction()
        {
            
        }

        #endregion

        private enum ActionEnum
        {
            // TODO Ajouter les actions possible pour correspondre à l'énoncé

            Nothing,
            Move,
            Reproduction
        }
    }
}
