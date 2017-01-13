using Microsoft.Xna.Framework;
using MultiAgentSystem.Constants;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Helpers.Graphics.Grids;
using MultiAgentSystem.Helpers.Grids;
using MultiAgentSystemV2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.Models.Agents
{
    internal class Agent : ICell
    {
        #region Properties

        public int Id { get; set; }
        public Coordinate Coordinate { get; set; }
        public int Movement { get; set; } = 1;
        public Color Color { get; set; } = Color.Silver;
        public System.Windows.Controls.Image Image { get; set; }
        public StateEnum State { get; set; } = StateEnum.Default;

        private Grid Grid { get; }
        private int ActionsNumber { get; } = 1;
        private Dictionary<DirectionEnum, object> Neighborhood { get; set; }
        private DirectionEnum CurrentDirection { get; set; }
        private Random Random { get; set; } = new Random();

        #endregion

        #region Construtors

        public Agent(Coordinate coordinate, Grid grid, int id = -1)
        {
            this.Coordinate = coordinate;
            this.Grid = grid;
            this.Id = id;
        }

        public Agent(Coordinate coordinate, Color color, Grid grid, int id = -1)
        {
            this.Coordinate = coordinate;
            this.Color = color;
            this.Grid = grid;
            this.Id = id;
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public void Decide()
        {
            CheckArround();

            int actionChoice = 0;//App.Random.Next(ActionsNumber);
            switch (actionChoice)
            {
                case 0:
                    for (int i = 0; i < Movement; i++)
                        ActionMove();
                    break;
                case 1:
                    ActionNothing();
                    break;
                default:
                    Logger.WriteLog("actionChoice selected doesn't exist or is not treated so agent decide to do nothing", LogLevelL4N.WARN);
                    ActionNothing();
                    break;
            }

            App.Trace(this.ToString());
        }

        private Dictionary<DirectionEnum, object> CheckArround()
        {

            Neighborhood = new Dictionary<DirectionEnum, object>();
            Coordinate NewCoordinate = null;

            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X, Coordinate.Y + 1);
            Neighborhood.Add(DirectionEnum.Bottom, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X, Coordinate.Y - 1);
            Neighborhood.Add(DirectionEnum.Top, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X + 1, Coordinate.Y);
            Neighborhood.Add(DirectionEnum.Right, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X - 1, Coordinate.Y);
            Neighborhood.Add(DirectionEnum.Left, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X - 1, Coordinate.Y + 1);
            Neighborhood.Add(DirectionEnum.TopRight, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X + 1, Coordinate.Y - 1);
            Neighborhood.Add(DirectionEnum.BottomLeft, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X - 1, Coordinate.Y - 1);
            Neighborhood.Add(DirectionEnum.TopLeft, Grid.Get(NewCoordinate.X, NewCoordinate.Y));
            NewCoordinate = RectifyCoordonate(App.IsToric, Coordinate.X + 1, Coordinate.Y + 1);
            Neighborhood.Add(DirectionEnum.BottomRight, Grid.Get(NewCoordinate.X, NewCoordinate.Y));

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
        /// To move the agent
        /// </summary>
        private void ActionMove()
        {
            // Choose the direction
            DirectionEnum direction = DecideDirection();
            // Free the current position
            Grid.Free(Coordinate);

            switch (direction)
            {
                case DirectionEnum.TopLeft:
                    Coordinate.X--;
                    Coordinate.Y--;
                    break;
                case DirectionEnum.Top:
                    Coordinate.Y--;
                    break;
                case DirectionEnum.BottomLeft:
                    Coordinate.X++;
                    Coordinate.Y--;
                    break;
                case DirectionEnum.Left:
                    Coordinate.X--;
                    break;
                case DirectionEnum.BottomRight:
                    Coordinate.X++;
                    Coordinate.Y++;
                    break;
                case DirectionEnum.Bottom:
                    Coordinate.Y++;
                    break;
                case DirectionEnum.TopRight:
                    Coordinate.X--;
                    Coordinate.Y++;
                    break;
                case DirectionEnum.Right:
                    Coordinate.X++;
                    break;
                case DirectionEnum.NoOne:
                    break;
                default:
                    Logger.WriteLog("Unknown direction : " + direction.ToString(), LogLevelL4N.ERROR);
                    break;
            }

            //Rectify Coordonate
            Coordinate = RectifyCoordonate(App.IsToric, Coordinate.X, Coordinate.Y);

            // Occupy the new position
            Grid.Occupy(Coordinate, this);
        }

        private void ActionNothing()
        {

        }

        private Coordinate RectifyCoordonate(bool Torique, int X, int Y)
        {
            Coordinate coord = new Coordinate(X, Y);
            if (!Torique) return coord;


            if (X <= -1)
            {
                coord.X = App.GridSizeX - 1;
            }
            else if (X >= App.GridSizeX)
            {
                coord.X = 0;
            }
            else if (Y <= -1)
            {
                coord.Y = App.GridSizeY - 1;
            }
            else if (Y >= App.GridSizeY)
            {
                coord.Y = 0;
            }

            return coord;
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
    }
}
