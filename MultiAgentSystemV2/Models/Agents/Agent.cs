using MultiAgentSystem.Constants;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Helpers.Grids;
using MultiAgentSystemV2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.Models.Agents
{
    internal class Agent : Cell
    {
        #region Properties

        public StateEnum State { get; set; } = StateEnum.Default;
        private Grid Grid { get; }
        private int ActionsNumber { get; } = 1;
        private Dictionary<DirectionEnum, Cell> Neighborhood { get; set; }
        private DirectionEnum CurrentDirection { get; set; }
        private Random Random { get; set; } = new Random();

        #endregion

        #region Construtors

        public Agent(Coordinate coordinate, Grid grid)
        {
            this.Coordinate = coordinate;
            this.Grid = grid;
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public void Decide()
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

        private Dictionary<DirectionEnum, Cell> CheckArround()
        {

            Neighborhood = new Dictionary<DirectionEnum, Cell>();

            Neighborhood.Add(DirectionEnum.Bottom, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Bottom, Coordinate)));
            Neighborhood.Add(DirectionEnum.Top, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Top, Coordinate)));
            Neighborhood.Add(DirectionEnum.Right, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Right, Coordinate)));
            Neighborhood.Add(DirectionEnum.Left, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Left, Coordinate)));
            Neighborhood.Add(DirectionEnum.TopRight, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.TopRight, Coordinate)));
            Neighborhood.Add(DirectionEnum.BottomLeft, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.BottomLeft, Coordinate)));
            Neighborhood.Add(DirectionEnum.TopLeft, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.TopLeft, Coordinate)));
            Neighborhood.Add(DirectionEnum.BottomRight, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.BottomRight, Coordinate)));

            return Neighborhood;
        }

        private DirectionEnum DecideDirection()
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
        private void ActionMove()
        {
            // Choose the direction
            DirectionEnum direction = DecideDirection();
            // Free the current position
            Grid.Free(Coordinate);

            Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);

            // Occupy the new position
            Grid.Occupy(Coordinate, this);
        }

        private void ActionNothing()
        {

        }

        /// <summary>
        /// This ToString is do simply to cast the object in a string formated in Json
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion

        private enum ActionEnum
        {
            Nothing,
            Move
        }
    }
}
