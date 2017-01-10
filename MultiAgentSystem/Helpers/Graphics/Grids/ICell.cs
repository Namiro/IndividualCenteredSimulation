using SharpDX;
using SharpDX.Direct2D1;

namespace MultiAgentSystem.Helpers.Graphics.Grids
{
    interface ICell
    {
        Coordinate Coordinate { get; set; }
        Color Color { get; set; }
        Image Image { get; set; }
    }
}
