using System.Windows.Controls;
using System.Windows.Media;

namespace IndividualCenteredSimulation.Helpers
{
    internal interface IDrawable
    {
        Color Color { get; set; }
        Image Image { get; set; }
    }
}
