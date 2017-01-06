namespace IndividualCenteredSimulation.Helpers
{
    public static class GraphicHelper
    {
        public static System.Windows.Media.Color CastColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

    }
}
