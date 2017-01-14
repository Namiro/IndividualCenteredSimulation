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
        protected int ActionsNumber { get; } = 1;
        protected Dictionary<DirectionEnum, Cell> Neighborhood { get; set; }
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
