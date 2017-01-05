using IndividualCenteredSimulation.MAS;

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

        #endregion

        public MultiAgentSystem MultiAgentSystem { get; set; }

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
                    case nameof(MultiAgentSystem.Agents):
                        RefereshView();
                        break;
                    default:
                        break;
                }
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method allow to refresh the view.
        /// It means to draw the view in function of the value of MultiAgentSystem.Agents
        /// </summary>
        public void RefereshView()
        {
            // TODO José Pansa
        }

        #endregion
    }
}
