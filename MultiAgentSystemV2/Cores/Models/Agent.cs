using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers.Grids;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.Cores.Models
{
    internal abstract class Agent : Cell, IAgent
    {
        #region Properties

        public Grid Grid { get; set; }
        protected int ActionsNumber { get; set; } = 1;
        protected Dictionary<DirectionEnum, Cell> Neighbors { get; set; }
        protected DirectionEnum CurrentDirection { get; set; }
        protected Random Random { get; set; } = new Random();

        #endregion

        #region Construtors

        public Agent()
        {

        }

        public Agent(Coordinate coordinate, Grid grid)
        {
            this.Coordinate = coordinate;
            this.Grid = grid;
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The algorithme of choice for decide the action and done this action.
        /// </summary>
        public abstract void Decide();

        /// <summary>
        /// Allow to know what we have arround the agent.
        /// </summary>
        /// <returns></returns>
        protected Dictionary<DirectionEnum, Cell> CheckArround()
        {

            Neighbors = new Dictionary<DirectionEnum, Cell>();

            Neighbors.Add(DirectionEnum.Down, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Down, Coordinate)));
            Neighbors.Add(DirectionEnum.Up, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Up, Coordinate)));
            Neighbors.Add(DirectionEnum.Right, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Right, Coordinate)));
            Neighbors.Add(DirectionEnum.Left, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.Left, Coordinate)));
            if (App.IsMoore)
            {
                Neighbors.Add(DirectionEnum.UpRight, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.UpRight, Coordinate)));
                Neighbors.Add(DirectionEnum.DownLeft, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.DownLeft, Coordinate)));
                Neighbors.Add(DirectionEnum.UpLeft, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.UpLeft, Coordinate)));
                Neighbors.Add(DirectionEnum.DownRight, Grid.Get(Grid.DirectionToCoordinate(DirectionEnum.DownRight, Coordinate)));
            }

            return Neighbors;
        }

        /// <summary>
        /// Alow to decide which direction the agent want take.
        /// </summary>
        /// <returns></returns>
        protected abstract DirectionEnum DecideDirection();

        /// <summary>
        /// To move the agent
        /// </summary>
        protected virtual void ActionMove()
        {
            // Choose the direction
            DirectionEnum direction = DecideDirection();
            // Free the current position
            Grid.Free(Coordinate);

            Coordinate = Grid.DirectionToCoordinate(direction, Coordinate);

            // Occupy the new position
            Grid.Occupy(this);
        }

        /// <summary>
        /// Action if you want to do nothing
        /// </summary>
        protected virtual void ActionNothing() { }

        /// <summary>
        /// This ToString is do simply to cast the object in a string formated in Json
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
