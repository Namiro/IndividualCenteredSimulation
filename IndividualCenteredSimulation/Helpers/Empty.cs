using System.Windows.Controls;
using System.Windows.Media;

namespace IndividualCenteredSimulation.Helpers
{
    internal class Empty : IDrawable
    {
        public Color Color { get; set; } = GraphicHelper.CastColor(System.Drawing.Color.Empty);
        public Image Image { get; set; } = null;
    }
}
