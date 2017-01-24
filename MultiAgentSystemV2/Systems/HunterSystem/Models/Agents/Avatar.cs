using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using MultiAgentSystem.HunterSystem.ViewModels;
using System.Collections.Generic;

namespace MultiAgentSystem.HunterSystem.Models
{
    internal class Avatar : Agent
    {
        #region Properties

        private static int DecideCount { get; set; } = 0;
        public int DefenderEaten { get; private set; } = 0;

        #endregion

        #region Construtors

        public Avatar()
        {
            ActionsNumber = 2;
            Color = Color.Transparent;
            Texture = XNAHunterGrid.ContentManager.Load<Texture2D>("avatar");
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public override void Decide()
        {
            if (++DecideCount % (101 - App.SpeedPercentAvatar) != 0)
                return;
            DecideCount = 0;

            CheckArround();

            ActionMove();

            Grid.CalculateDijkstra(this.Coordinate);
        }

        protected override DirectionEnum DecideDirection()
        {

            List<DirectionEnum> possibleDirections = new List<DirectionEnum>();
            foreach (var Neighbor in Neighbors)
            {
                // Si cellule pas null et est de type Empty, c'est que on est pas a une frontière et que la case n'est pas occupée.
                if (Neighbor.Value != null && (Neighbor.Value is Empty || Neighbor.Value is Defender || Neighbor.Value is WinnerDiamond))
                    possibleDirections.Add(Neighbor.Key);
            }

            // Si la direction choisie par l'utilisateur est possible on utilise cette direction, sinon on s'arrete de bouger
            if (possibleDirections.Contains(XNAHunterGrid.UserDirectionChoose))
            {
                CurrentDirection = XNAHunterGrid.UserDirectionChoose;
                XNAHunterGrid.UserDirectionChoose = DirectionEnum.NoOne;
                return CurrentDirection;
            }


            return DirectionEnum.NoOne;
        }

        /// <summary>
        /// To move the agent
        /// </summary>
        protected override void ActionMove()
        {
            // Choose the direction
            DirectionEnum direction = DecideDirection();

            if (direction != DirectionEnum.NoOne)
            {
                // Free the current position
                Grid.Free(Coordinate);

                Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);

                // Occupy the new position
                Cell PreviousObj = Grid.Occupy(this);

                // In function of the object that the avatar erase/take
                if (PreviousObj is Defender)
                    DefenderEaten++;
                else if (PreviousObj is WinnerDiamond)
                    XNAHunterGrid.IsWinner = true;
            }
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
