using IndividualCenteredSimulation.Constants;
using System.Drawing;

namespace IndividualCenteredSimulation.Agents
{
    internal class Agent
    {
        #region Properties

        public System.Windows.Point Coordinate { get; set; }
        public int Movement { get; set; } = 1;
        public Color Color { get; set; } = Color.Black;

        private StateEnum State { get; set; } = StateEnum.Default;

        #endregion

        #region Construtors

        public Agent(System.Windows.Point coordinate)
        {
            this.Coordinate = coordinate;
        }

        public Agent(System.Windows.Point coordinate, Color color)
        {
            this.Coordinate = coordinate;
            this.Color = color;
        }

        #endregion

        #region Methodes

        public void Update() { }

        /// <summary>
        /// The behaviour when the agent meet the border or another agent
        /// </summary>
        public void Decide() { }

        #endregion
    }
}
