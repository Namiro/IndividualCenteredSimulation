using IndividualCenteredSimulation.Helpers;
using IndividualCenteredSimulation.MAS;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace IndividualCenteredSimulation.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        private DrawingImage _SurfaceGrid;
        public DrawingImage SurfaceGrid
        {
            get
            {
                return _SurfaceGrid;
            }
            set
            {
                _SurfaceGrid = value;
                RaisePropertyChanged(nameof(MainWindowViewModel.SurfaceGrid));
            }
        }

        public MultiAgentSystem MultiAgentSystem { get; set; }
        private GraphicHelperGrid GraphicHelperGrid { get; set; }

        #endregion

        #region Construtors

        public MainWindowViewModel()
        {
            MultiAgentSystem = new MultiAgentSystem();

            //This allow to refresh the data to display when the value from the system is modified
            MultiAgentSystem.PropertyChanged += (obj, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(MultiAgentSystem.Grid):
                        RefereshView();
                        break;
                    default:
                        break;
                }
            };


            GraphicHelperGrid = new GraphicHelperGrid(MultiAgentSystem.Grid, SurfaceGrid);
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method allow to refresh the view.
        /// It means to draw the view in function of the value of MultiAgentSystem.Agents
        /// </summary>
        public void RefereshView()
        {
            // This line allow to clear the event queue managed by the .NET Framework.
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                this.GraphicHelperGrid.Draw();
            }));
        }

        #endregion
    }
}
