using IndividualCenteredSimulation.Helpers;
using IndividualCenteredSimulation.MAS;
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

        private List<List<IDrawable>> _Grid = new List<List<IDrawable>>();
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
            for (int i = 0; i < MultiAgentSystem.Grid.YSize - 1; i++)
            {
                Grid.Add(new List<IDrawable>());
                for (int j = 0; j < MultiAgentSystem.Grid.XSize - 1; j++)
                {
                    if (MultiAgentSystem.Grid.YSize >= MultiAgentSystem.Grid.XSize)
                        Grid[i].Add(((IDrawable)MultiAgentSystem.Grid.Get(i, j)));
                    else
                        Grid[i].Add(((IDrawable)MultiAgentSystem.Grid.Get(j, i)));
                }

            }
        }

        #endregion
    }
}
