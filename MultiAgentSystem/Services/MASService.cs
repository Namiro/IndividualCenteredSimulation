using MultiAgentSystem.Environments;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Helpers.Graphics.Grids;
using MultiAgentSystem.Helpers.Services;

namespace MultiAgentSystem.Services
{
    internal class MASService : Service
    {
        #region Properties

        public Environment Environment { get; set; }
        private int DrawTimeNb { get; set; } = 0;
        private int DrawTimeSum { get; set; } = 0;
        private int DrawTimeBigestNb { get; set; } = 0;
        private int DrawBiggestTimeSum { get; set; } = 0;
        private int DrawTimeBiggest { get; set; } = 0;

        #endregion

        #region Construtors

        public MASService()
        {
            Environment = new Environment();

            //This allow to refresh the data to display when the value from the system is modified
            Environment.PropertyChanged += (obj, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(Environment.Grid):
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
            App.StartExec = System.DateTime.Now;


            if (App.IsTracedPerformance)
            {
                DrawTimeNb++;
                int drawTime = System.DateTime.Now.Subtract(App.StartExec).Milliseconds;
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
        }

        #endregion
    }
}
