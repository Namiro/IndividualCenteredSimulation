using IndividualCenteredSimulation.Helpers;
using IndividualCenteredSimulation.MAS;

namespace IndividualCenteredSimulation.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        public static MultiAgentSystem MultiAgentSystem { get; set; }
        public static GraphicHelperGridSharpDX GraphicHelperGridSharpDX { get; set; }

        #endregion

        #region Construtors

        public MainWindowViewModel()
        {
            MultiAgentSystem = new MultiAgentSystem();
        }

        #endregion

        #region Methods

        public static void Initialize(GraphicHelperGridSharpDX graphicHelperGridSharpDX)
        {
            GraphicHelperGridSharpDX = graphicHelperGridSharpDX;
            GraphicHelperGridSharpDX.Grid = MultiAgentSystem.Grid;
            GraphicHelperGridSharpDX.IsDisplayAxeNum = false;
            GraphicHelperGridSharpDX.IsDisplayGrid = App.IsDisplayGrid;
        }

        #endregion
    }
}
