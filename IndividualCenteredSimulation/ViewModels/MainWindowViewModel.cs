using IndividualCenteredSimulation.Helpers;
using IndividualCenteredSimulation.MAS;
using System;
using System.Collections.Generic;

namespace IndividualCenteredSimulation.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region Constants

        #endregion

        #region Commands

        #endregion

        #region Events

        #endregion

        #region GUI

        private List<List<IDrawable>> _Grid;
        public List<List<IDrawable>> Grid
        {
            get
            {
                return _Grid;
            }
            set
            {
                _Grid = value;
                RaisePropertyChanged(nameof(MainWindowViewModel.Grid));
            }
        }

        public double BoxSize { get; set; } = Constants.Constants.DEFAULT_BOX_SIZE;

        #endregion

        public MultiAgentSystem MultiAgentSystem { get; set; }

        #endregion

        #region Construtors

        public MainWindowViewModel()
        {
            BoxSize = App.BoxSize;

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

            RefereshView();
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

            Grid = new List<List<IDrawable>>();

            for (int i = 0; i < MultiAgentSystem.Grid.XSize; i++)
            {
                List<IDrawable> line = new List<IDrawable>();
                Grid.Add(line);
                for (int j = 0; j < MultiAgentSystem.Grid.YSize; j++)
                    line.Add(((IDrawable)MultiAgentSystem.Grid.Get(i, j)));
            }

            Logger.WriteLog("Draw time : " + DateTime.Now.Subtract(App.StartExec).Milliseconds);
        }

        #endregion
    }
}
