using Helpers.Services;
using IndividualCenteredSimulation.Agents;

namespace IndividualCenteredSimulation.MAS
{
    internal class MultiAgentSystem : Service
    {
        #region Properties

        public Agent[][] Agents { get; set; }

        #endregion

        #region Construtors

        public MultiAgentSystem()
        {

        }

        #endregion

        #region Methodes

        public void run()
        {

            RaisePropertyChanged(nameof(MultiAgentSystem.Agents));
        }

        #endregion
    }
}
