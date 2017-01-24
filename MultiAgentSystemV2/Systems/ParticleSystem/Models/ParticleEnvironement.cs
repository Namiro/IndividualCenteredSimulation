namespace MultiAgentSystem.ParticleSystem.Models
{
    internal class ParticleEnvironment : Cores.Models.Environment
    {
        #region Properties



        #endregion

        #region Construtors

        public ParticleEnvironment() : base()
        {
            for (int i = 0; i < App.ParticlesNumber; i++)
                Agents.Add(new Particle());

            this.Initialize();
        }

        #endregion

        #region Methodes



        #endregion
    }
}
