using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using MultiAgentSystem.ParticleSystem.ViewModels;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.ParticleSystem.Models
{
    internal class Particle : Agent
    {
        #region Properties


        #endregion

        #region Construtors

        public Particle()
        {
            Color = new Color((float)App.Random.NextDouble(), (float)App.Random.NextDouble(), (float)App.Random.NextDouble());
            Texture = XNAParticleGrid.ContentManager.Load<Texture2D>("circle");
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

        }

        protected override DirectionEnum DecideDirection()
        {

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
            // Choose the direction
            DirectionEnum direction = DecideDirection();
            // Free the current position
            Grid.Free(Coordinate);

            Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);

            // Occupy the new position
            Grid.Occupy(this);
        }

        protected override void ActionNothing()
        {

        }

        #endregion

        private enum ActionEnum
        {
            Nothing,
            Move
        }
    }
}
