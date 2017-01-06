using IndividualCenteredSimulation.Constants;
using IndividualCenteredSimulation.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace IndividualCenteredSimulation.Agents
{
    internal class Agent : IDrawable
    {
        #region Properties

        public Coordinate Coordinate { get; set; }
        public int Movement { get; set; } = 1;
        public Color Color { get; set; } = GraphicHelper.CastColor(System.Drawing.Color.Silver);
        public System.Windows.Controls.Image Image { get; set; }
        public StateEnum State { get; set; } = StateEnum.Default;

        private Grid Grid { get; }
        private static int ActionsNumber { get; } = 2;
        private Dictionary<DirectionEnum, object> Neighborhood { get; set; }

        #endregion

        #region Construtors

        public Agent(Coordinate coordinate, Grid grid)
        {
            this.Coordinate = coordinate;
            this.Grid = grid;
        }

        public Agent(Coordinate coordinate, Color color, Grid grid)
        {
            this.Coordinate = coordinate;
            this.Color = color;
            this.Grid = grid;
        }

        #endregion

        #region Methodes

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public void Decide()
        {
            Random random = new Random();

            CheckArround();

            int actionChoice = random.Next(ActionsNumber);
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
        }

        private Dictionary<DirectionEnum, object> CheckArround()
        {
            Neighborhood = new Dictionary<DirectionEnum, object>();

            Neighborhood.Add(DirectionEnum.TopLeft, Grid.Get(Coordinate.X - 1, Coordinate.Y - 1));
            Neighborhood.Add(DirectionEnum.Top, Grid.Get(Coordinate.X, Coordinate.Y - 1));
            Neighborhood.Add(DirectionEnum.TopRight, Grid.Get(Coordinate.X + 1, Coordinate.Y - 1));
            Neighborhood.Add(DirectionEnum.Right, Grid.Get(Coordinate.X + 1, Coordinate.Y));
            Neighborhood.Add(DirectionEnum.BottomRight, Grid.Get(Coordinate.X + 1, Coordinate.Y + 1));
            Neighborhood.Add(DirectionEnum.Bottom, Grid.Get(Coordinate.X, Coordinate.Y + 1));
            Neighborhood.Add(DirectionEnum.BottomLeft, Grid.Get(Coordinate.X - 1, Coordinate.Y + 1));
            Neighborhood.Add(DirectionEnum.Left, Grid.Get(Coordinate.X - 1, Coordinate.Y));

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

            // Mélange les possibilités.
            Helper.Shuffle(possibleDirections);
            return possibleDirections[0];
        }

        /// <summary>
        /// 
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
                case DirectionEnum.TopRight:
                    Coordinate.X++;
                    Coordinate.Y--;
                    break;
                case DirectionEnum.Right:
                    Coordinate.X--;
                    break;
                case DirectionEnum.BottomRight:
                    Coordinate.X++;
                    Coordinate.Y++;
                    break;
                case DirectionEnum.Bottom:
                    Coordinate.Y++;
                    break;
                case DirectionEnum.BottomLeft:
                    Coordinate.X--;
                    Coordinate.Y++;
                    break;
                case DirectionEnum.Left:
                    Coordinate.X--;
                    break;
                default:
                    Logger.WriteLog("Unknown direction : " + direction.ToString(), LogLevelL4N.ERROR);
                    break;
            }

            // Occupy the new position
            Grid.Occupy(Coordinate, this);
        }

        private void ActionNothing()
        {

        }

        #endregion
    }
}
