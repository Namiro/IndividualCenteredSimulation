using MultiAgentSystem.Helpers.Graphics.Grids;
using SharpDX;
using SharpDX.Direct2D1;

namespace MultiAgentSystem.Helpers
{
    internal class Empty : ICell
    {
        public Coordinate Coordinate { get; set; }
        public Color Color { get; set; } = Color.White;
        public Image Image { get; set; }
    }
}
