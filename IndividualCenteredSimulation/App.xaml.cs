using IndividualCenteredSimulation.Constants;
using IndividualCenteredSimulation.Helpers;
using System.Configuration;
using System.Windows;

namespace IndividualCenteredSimulation
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Properties

        /// <summary>
        /// The number of case in X.
        /// </summary>
        public int GridSizeX { get; set; }
        /// <summary>
        /// The number of case in Y.
        /// </summary>
        public int GridSizeY { get; set; }

        /// <summary>
        /// The panel size in pixels for the axe X
        /// </summary>
        public int CanvasSizeX { get; set; }

        /// <summary>
        /// The panel size in pixels for the axe Y.
        /// </summary>
        public int CanvasSizeY { get; set; }

        /// <summary>
        /// The delay in milliseconde between each tick.
        /// </summary>
        public long DelayMilliseconde { get; set; }

        /// <summary>
        /// The box size in pixels (Box = Cell).
        /// </summary>
        public int BoxSize { get; set; }

        /// <summary>
        /// The strategy used for the scheduling. See SchedulingStrategyEnum.
        /// </summary>
        public SchedulingStrategyEnum SchedulingStrategy { get; set; }

        /// <summary>
        /// The number of turn of speech. 
        /// 0 = Unlimited.
        /// </summary>
        public long TicksNumber { get; set; }

        /// <summary>
        /// Display or not the grid.
        /// </summary>
        public bool IsDisplayGrid { get; set; }

        /// <summary>
        /// Trace or not the app.
        /// </summary>
        public bool IsTraced { get; set; }

        /// <summary>
        /// The seed for the random. 
        /// 0 = Random.
        /// </summary>
        public int Seed { get; set; }

        /// <summary>
        /// Rate of refresh. After how many tick we resfresh the view.
        /// </summary>
        public int RateRefresh { get; set; }

        /// <summary>
        /// The number of agent we will simulate.
        /// </summary>
        public int AgentNumber { get; set; }


        #endregion

        #region Methodes



        /// <summary>
        /// Initialize settings.
        /// </summary>
        private void LoadSettings()
        {
            GridSizeX = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_GRID_SIZE_X]);
            GridSizeY = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_GRID_SIZE_Y]);
            CanvasSizeX = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_CANVAS_SIZE_X]);
            CanvasSizeY = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_CANVAS_SIZE_Y]);
            BoxSize = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_BOX_SIZE]);
            DelayMilliseconde = long.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_DELAY_MILLISECONDE]);
            TicksNumber = long.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_TICKS_NUMBER]);
            Seed = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SEED]);
            RateRefresh = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_RATE_REFRESH]);

            // IsDisplayGrid
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_DISPLAY_GRID] == "True")
                IsDisplayGrid = true;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_DISPLAY_GRID] == "False")
                IsDisplayGrid = false;
            else
            {
                IsDisplayGrid = Constants.Constants.DEFAULT_IS_DISPLAY_GRID;
                Logger.WriteLog(Logger.CONSOLE, "The IsDisplayGrid value is unknown. Default value will be used", LogLevelL4N.WARN);
            }

            // IsTraced
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_TRACED] == "True")
                IsTraced = true;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_TRACED] == "False")
                IsTraced = false;
            else
            {
                IsTraced = Constants.Constants.DEFAULT_IS_TRACED;
                Logger.WriteLog(Logger.CONSOLE, "The IsTraced value is unknown. Default value will be used", LogLevelL4N.WARN);
            }

            // SchedulingStrategy
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Fair.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Fair;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Sequential.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Sequential;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Random.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Random;
            else
            {
                SchedulingStrategy = Constants.Constants.DEFAULT_SCHEDULING_STRATEGY;
                Logger.WriteLog(Logger.CONSOLE, "The SchedulingStrategy value is unknown. Default value will be used", LogLevelL4N.WARN);
            }
        }


        #endregion
    }
}
