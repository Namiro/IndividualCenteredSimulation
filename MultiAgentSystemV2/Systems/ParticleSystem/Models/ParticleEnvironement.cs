using MultiAgentSystem.Cores.Helpers;

namespace MultiAgentSystem.ParticleSystem.Models
{
    internal class ParticleEnvironment : Cores.Models.Environment
    {
        #region Properties



        #endregion

        #region Construtors

        public ParticleEnvironment() : base()
        {
            if (App.GridSizeX * App.GridSizeY < App.ParticlesNumber)
            {
                Logger.WriteLog("Particle number to big to match with the grid size.", LogLevelL4N.FATAL);
                throw new System.Exception("Particle number to big to match with the grid size.");
            }

            for (int i = 0; i < App.ParticlesNumber; i++)
                Agents.Add(new Particle());

            this.Initialize();
        }

        #endregion

        #region Methodes



        #endregion
    }
}
