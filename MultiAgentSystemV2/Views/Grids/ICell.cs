
using Microsoft.Xna.Framework;
using System.Windows.Controls;

namespace MultiAgentSystem.Helpers.Graphics.Grids
{
    interface ICell
    {
        Coordinate Coordinate { get; set; }
        Color Color { get; set; }
        Image Image { get; set; }
    }
}
