namespace MultiAgentSystem.Helpers.Graphics
{
    public class GraphicHelper
    {
        #region Properties



        #endregion

        #region Constructors

        public GraphicHelper()
        {

        }

        #endregion

        #region Methods



        #endregion

        #region Static Methods

        public static System.Windows.Media.Color CastColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        #endregion
    }
}
