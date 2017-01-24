using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using MultiAgentSystem.HunterSystem.ViewModels;
using System.Collections.Generic;

namespace MultiAgentSystem.HunterSystem.Models
{
    internal class Hunter : Agent
    {
        #region Properties

        private static int DecideCount { get; set; } = 0;

        #endregion

        #region Construtors

        public Hunter()
        {
            ActionsNumber = 2;
            Color = Color.Transparent;
            Texture = XNAHunterGrid.ContentManager.Load<Texture2D>("hunter");
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public override void Decide()
        {
            if (++DecideCount % (101 - App.SpeedPercentHunter) != 0)
                return;
            DecideCount = 0;

            CheckArround();

            CheckGameOver();
            if (XNAHunterGrid.IsGameOver)
                return;

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

        private void CheckGameOver()
        {
            foreach (var elem in Neighbors)
            {
                // Si cellule pas null et est de type Empty, c'est que on est pas a une frontière et que la case n'est pas occupée.
                if (elem.Value != null && elem.Value is Avatar)
                    XNAHunterGrid.IsGameOver = true;
            }
        }

        protected override DirectionEnum DecideDirection()
        {
            List<DirectionEnum> possibleDirections = new List<DirectionEnum>();
            foreach (var elem in Neighbors)
            {
                // Si cellule pas null et est de type Empty, c'est que on est pas a une frontière et que la case n'est pas occupée.
                if (elem.Value != null && elem.Value is Empty)
                    possibleDirections.Add(elem.Key);
            }

            // On prend la direction qui a l'indice de drijska le plus faible.
            DirectionEnum returnedDirection = DirectionEnum.NoOne;
            if (!XNAHunterGrid.IsSuperAvatar)
            {
                int previousDijkstraValue = int.MaxValue;
                foreach (DirectionEnum direction in possibleDirections)
                {
                    int dijkstraValue = Grid.Get(Grid.DirectionToCoordinate(direction, Coordinate)).DijkstraValue;
                    if (previousDijkstraValue > dijkstraValue && dijkstraValue != -1)
                    {
                        previousDijkstraValue = dijkstraValue;
                        returnedDirection = direction;
                    }
                }
            }
            else
            {
                int previousDijkstraValue = int.MinValue;
                foreach (DirectionEnum direction in possibleDirections)
                {
                    int dijkstraValue = Grid.Get(Grid.DirectionToCoordinate(direction, Coordinate)).DijkstraValue;
                    if (previousDijkstraValue < dijkstraValue && dijkstraValue != -1)
                    {
                        previousDijkstraValue = dijkstraValue;
                        returnedDirection = direction;
                    }
                }
            }

            return returnedDirection;
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
