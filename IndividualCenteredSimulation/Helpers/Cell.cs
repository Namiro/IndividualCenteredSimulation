using SharpDX.Mathematics.Interop;
using System.Windows.Controls;

namespace IndividualCenteredSimulation.Helpers
{
    internal abstract class Cell : IDrawable
    {
        public abstract RawColor4 Color { get; set; }
        public abstract Image Image { get; set; }
    }
}
