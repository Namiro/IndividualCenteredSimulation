using IndividualCenteredSimulation.Constants;
using System;
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
        /// Calcul time execution
        /// </summary>
        public static DateTime StartExec { get; set; }

        /// <summary>
        /// The number of case in X.
        /// </summary>
        public static int GridSizeX { get; set; } = Constants.Constants.DEFAULT_GRID_SIZE_X;
        /// <summary>
        /// The number of case in Y.
        /// </summary>
        public static int GridSizeY { get; set; } = Constants.Constants.DEFAULT_GRID_SIZE_Y;

        /// <summary>
        /// The panel size in pixels for the axe X
        /// </summary>
        public static int CanvasSizeX { get; set; } = Constants.Constants.DEFAULT_CANVAS_SIZE_X;

        /// <summary>
        /// The panel size in pixels for the axe Y.
        /// </summary>
        public static int CanvasSizeY { get; set; } = Constants.Constants.DEFAULT_CANVAS_SIZE_Y;

        /// <summary>
        /// The delay in milliseconde between each tick.
        /// </summary>
        public static int DelayMilliseconde { get; set; } = Constants.Constants.DEFAULT_DELAY_MILLISECONDE;

        /// <summary>
        /// The box size in pixels (Box = Cell).
        /// </summary>
        public static int BoxSize { get; set; } = Constants.Constants.DEFAULT_BOX_SIZE;

        /// <summary>
        /// The strategy used for the scheduling. See SchedulingStrategyEnum.
        /// </summary>
        public static SchedulingStrategyEnum SchedulingStrategy { get; set; } = Constants.Constants.DEFAULT_SCHEDULING_STRATEGY;

        /// <summary>
        /// The number of turn of speech. 
        /// 0 = Unlimited.
        /// </summary>
        public static int TicksNumber { get; set; } = Constants.Constants.DEFAULT_TICKS_NUMBER;

        /// <summary>
        /// Display or not the grid.
        /// </summary>
        public static bool IsDisplayGrid { get; set; } = Constants.Constants.DEFAULT_IS_DISPLAY_GRID;

        /// <summary>
        /// Trace or not the app.
        /// </summary>
        public static bool IsTraced { get; set; } = Constants.Constants.DEFAULT_IS_TRACED;

        /// <summary>
        /// The seed for the random. 
        /// 0 = Random.
        /// </summary>
        public static int Seed { get; set; } = Constants.Constants.DEFAULT_SEED;

        /// <summary>
        /// Rate of refresh. After how many tick we resfresh the view.
        /// </summary>
        public static int RateRefresh { get; set; } = Constants.Constants.DEFAULT_RATE_REFRESH;

        /// <summary>
        /// The number of agent we will simulate.
        /// </summary>
        public static int AgentsNumber { get; set; } = Constants.Constants.DEFAULT_AGENTS_NUMBER;

        /// <summary>
        /// Trace or not the app performance.
        /// </summary>
        public static bool IsTracedPerformance { get; set; } = Constants.Constants.DEFAULT_IS_TRACED_PERFORMANCE;


        #endregion

        #region Methodes



        /// <summary>
        /// Initialize settings.
        /// </summary>
        public static void LoadSettings()
        {
            GridSizeX = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_GRID_SIZE_X]);
            GridSizeY = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_GRID_SIZE_Y]);
            CanvasSizeX = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_CANVAS_SIZE_X]);
            CanvasSizeY = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_CANVAS_SIZE_Y]);
            BoxSize = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_BOX_SIZE]);
            DelayMilliseconde = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_DELAY_MILLISECONDE]);
            TicksNumber = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_TICKS_NUMBER]);
            Seed = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SEED]);
            RateRefresh = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_RATE_REFRESH]);
            AgentsNumber = int.Parse(ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_AGENTS_NUMBER]);

            // IsDisplayGrid
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_DISPLAY_GRID] == "True")
                IsDisplayGrid = true;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_DISPLAY_GRID] == "False")
                IsDisplayGrid = false;

            // IsTraced
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_TRACED] == "True")
                IsTraced = true;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_TRACED] == "False")
                IsTraced = false;

            // IsTraced
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_TRACED_PERFORMANCE] == "True")
                IsTraced = true;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_IS_TRACED_PERFORMANCE] == "False")
                IsTraced = false;


            // SchedulingStrategy
            if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Fair.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Fair;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Sequential.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Sequential;
            else if (ConfigurationManager.AppSettings[Constants.Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Random.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Random;
        }


        #endregion
    }
}
