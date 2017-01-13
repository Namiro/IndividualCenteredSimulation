using Microsoft.Xna.Framework;
using MultiAgentSystem.Helpers.Graphics.Grids;
using System.Windows.Controls;

namespace MultiAgentSystem.Helpers.Grids
{
    internal class Empty : ICell
    {
        public Coordinate Coordinate { get; set; }
        public Color Color { get; set; } = Color.White;
        public Image Image { get; set; }
    }
}
