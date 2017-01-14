namespace MultiAgentSystem.WatorSystem.Models
{
    internal class WatorEnvironment : Cores.Models.Environment
    {
        #region Properties



        #endregion

        #region Construtors

        public WatorEnvironment() : base()
        {
            for (int i = 0; i < App.FishsNumber; i++)
                Agents.Add(new Fish());

            for (int i = 0; i < App.SharksNumber; i++)
                Agents.Add(new Shark());

            this.Initialize(Agents);
        }

        #endregion

        #region Methodes



        #endregion
    }
}
