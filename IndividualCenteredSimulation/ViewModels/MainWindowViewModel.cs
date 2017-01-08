using IndividualCenteredSimulation.Helpers;
using IndividualCenteredSimulation.MAS;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace IndividualCenteredSimulation.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        private WriteableBitmap _SurfaceGridBitmap;
        public WriteableBitmap SurfaceGridBitmap
        {
            get
            {
                return _SurfaceGridBitmap;
            }
            set
            {
                _SurfaceGridBitmap = value;
                RaisePropertyChanged(nameof(MainWindowViewModel.SurfaceGridBitmap));
            }
        }

        public MultiAgentSystem MultiAgentSystem { get; set; }
        private GraphicHelperGridEx GraphicHelperGridEx { get; set; }
        private int DrawTimeNb { get; set; } = 0;
        private int DrawTimeSum { get; set; } = 0;
        private int DrawTimeBigestNb { get; set; } = 0;
        private int DrawBiggestTimeSum { get; set; } = 0;
        private int DrawTimeBiggest { get; set; } = 0;

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

            GraphicHelperGridEx = new GraphicHelperGridEx(MultiAgentSystem.Grid);
            GraphicHelperGridEx.IsDisplayAxeNum = App.IsDisplayAxe;
            GraphicHelperGridEx.IsDisplayGrid = App.IsDisplayGrid;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method allow to refresh the view.
        /// It means to draw the view in function of the value of MultiAgentSystem.Agents
        /// </summary>
        public void RefereshView()
        {
            App.StartExec = DateTime.Now;

            // This line allow to clear the event queue managed by the .NET Framework.
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                GraphicHelperGridEx.Draw();
                SurfaceGridBitmap = GraphicHelperGridEx.WriteableBitmap;

                if (App.IsTracedPerformance)
                {
                    DrawTimeNb++;
                    int drawTime = DateTime.Now.Subtract(App.StartExec).Milliseconds;
                    DrawTimeSum += drawTime;
                    if (DrawTimeBiggest < drawTime)
                        DrawTimeBiggest = drawTime;

                    int averageTime = DrawTimeSum / DrawTimeNb;

                    if (drawTime <= DrawTimeBiggest && drawTime > averageTime)
                    {
                        DrawTimeBigestNb++;
                        DrawBiggestTimeSum += drawTime;
                    }

                    int averageBiggestTime = DrawBiggestTimeSum / DrawTimeBigestNb;
                    Logger.WriteLog("Draw time: " + drawTime + "\tDraw time average : " + averageTime + "\tSlowest : " + DrawTimeBiggest + "\tAverage biggest :" + averageBiggestTime);
                }
            }));
        }

        #endregion
    }
}
