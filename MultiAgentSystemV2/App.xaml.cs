using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers;
using System;
using System.Configuration;
using System.Globalization;
using System.Windows;

namespace MultiAgentSystem
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Properties

        #region Config Properties

        /// <summary>
        /// Calcul time execution
        /// </summary>
        public static System.DateTime StartExec { get; set; }

        /// <summary>
        /// The number of case in X.
        /// </summary>
        public static int GridSizeX { get; set; } = Constants.DEFAULT_GRID_SIZE_X;
        /// <summary>
        /// The number of case in Y.
        /// </summary>
        public static int GridSizeY { get; set; } = Constants.DEFAULT_GRID_SIZE_Y;

        /// <summary>
        /// The panel size in pixels for the axe X
        /// </summary>
        public static int CanvasSizeX { get; set; } = Constants.DEFAULT_CANVAS_SIZE_X;

        /// <summary>
        /// The panel size in pixels for the axe Y.
        /// </summary>
        public static int CanvasSizeY { get; set; } = Constants.DEFAULT_CANVAS_SIZE_Y;

        /// <summary>
        /// The delay in milliseconde between each tick.
        /// </summary>
        public static int DelayMilliseconde { get; set; } = Constants.DEFAULT_DELAY_MILLISECONDE;

        /// <summary>
        /// The box size in pixels (Box = Cell).
        /// </summary>
        public static int BoxSize { get; set; } = Constants.DEFAULT_BOX_SIZE;

        /// <summary>
        /// The strategy used for the scheduling. See SchedulingStrategyEnum.
        /// </summary>
        public static SchedulingStrategyEnum SchedulingStrategy { get; set; } = Constants.DEFAULT_SCHEDULING_STRATEGY;

        /// <summary>
        /// The number of turn of speech. 
        /// 0 = Unlimited.
        /// </summary>
        public static int TicksNumber { get; set; } = Constants.DEFAULT_TICKS_NUMBER;

        /// <summary>
        /// Display or not the grid.
        /// </summary>
        public static bool IsDisplayGrid { get; set; } = Constants.DEFAULT_IS_DISPLAY_GRID;

        /// <summary>
        /// Trace or not the app.
        /// </summary>
        public static bool IsTraced { get; set; } = Constants.DEFAULT_IS_TRACED;

        /// <summary>
        /// The seed for the random. 
        /// 0 = Random.
        /// </summary>
        public static int Seed { get; set; } = Constants.DEFAULT_SEED;

        /// <summary>
        /// Rate of refresh. After how many tick we resfresh the view.
        /// </summary>
        public static int RateRefresh { get; set; } = Constants.DEFAULT_RATE_REFRESH;

        /// <summary>
        /// The number of particles we will simulate.
        /// </summary>
        public static int ParticlesNumber { get; set; } = Constants.DEFAULT_PARTICLES_NUMBER;

        /// <summary>
        /// Trace or not the app performance.
        /// </summary>
        public static bool IsTracedPerformance { get; set; } = Constants.DEFAULT_IS_TRACED_PERFORMANCE;

        /// <summary>
        /// Display or not the axe.
        /// </summary>
        public static bool IsDisplayAxe { get; set; } = Constants.DEFAULT_IS_DISPLAY_AXE;

        /// <summary>
        /// Display or not the axe.
        /// </summary>
        public static bool IsToric { get; set; } = Constants.DEFAULT_IS_TORIC;

        /// <summary>
        /// Display or not the axe.
        /// </summary>
        public static int FishsNumber { get; set; } = Constants.DEFAULT_FISHS_NUMBER;

        /// <summary>
        /// Display or not the axe.
        /// </summary>
        public static int SharksNumber { get; set; } = Constants.DEFAULT_SHARKS_NUMBER;

        public static int HuntersNumber { get; set; } = Constants.DEFAULT_HUNTERS_NUMBER;
        public static int WallsPercent { get; set; } = Constants.DEFAULT_WALLS_PERCENT;
        public static int WallsSizeMin { get; set; } = Constants.DEFAULT_WALLS_SIZE_MIN;
        public static int WallsSizeMax { get; set; } = Constants.DEFAULT_WALLS_SIZE_MAX;
        public static int SpeedPercentHunter { get; set; } = Constants.DEFAULT_SPEED_PERCENT_HUNTER;
        public static int SpeedPercentAvatar { get; set; } = Constants.DEFAULT_SPEED_PERCENT_AVATAR;
        public static int DefenderLife { get; set; } = Constants.DEFAULT_DEFENDER_LIFE;
        public static bool IsMoore { get; set; } = Constants.DEFAULT_IS_MOORE;

        #endregion

        #endregion

        public static Random Random { get; private set; }

        #region Methodes

        public static void Trace(string text)
        {
            if (App.IsTraced)
            {
                Logger.WriteLog(text, LogLevelL4N.INFO);
            }
        }

        /// <summary>
        /// Initialize settings.
        /// </summary>
        public static void LoadSettings()
        {
            GridSizeX = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_GRID_SIZE_X]);
            GridSizeY = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_GRID_SIZE_Y]);
            CanvasSizeX = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_CANVAS_SIZE_X]);
            CanvasSizeY = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_CANVAS_SIZE_Y]);
            BoxSize = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_BOX_SIZE]);
            DelayMilliseconde = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_DELAY_MILLISECONDE]);
            TicksNumber = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_TICKS_NUMBER]);
            RateRefresh = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_RATE_REFRESH]);
            ParticlesNumber = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_PARTICLES_NUMBER]);
            Seed = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SEED]);
            SharksNumber = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SHARKS_NUMBER]);
            FishsNumber = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_FISHS_NUMBER]);

            HuntersNumber = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_HUNTERS_NUMBER]);
            WallsPercent = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_WALLS_PERCENT], CultureInfo.InvariantCulture.NumberFormat);
            WallsSizeMin = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_WALLS_SIZE_MIN]);
            WallsSizeMax = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_WALLS_SIZE_MAX]);
            SpeedPercentHunter = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SPEED_PERCENT_HUNTER], CultureInfo.InvariantCulture.NumberFormat);
            SpeedPercentAvatar = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SPEED_PERCENT_AVATAR], CultureInfo.InvariantCulture.NumberFormat);
            DefenderLife = int.Parse(ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_DEFENDER_LIFE]);

            // Percent (0 - 100)
            if (WallsPercent > 100 || WallsPercent < 0)
                WallsPercent = Constants.DEFAULT_WALLS_PERCENT;
            if (SpeedPercentHunter > 100 || SpeedPercentHunter < 0)
                SpeedPercentHunter = Constants.DEFAULT_SPEED_PERCENT_HUNTER;
            if (SpeedPercentAvatar > 100 || SpeedPercentAvatar < 0)
                SpeedPercentAvatar = Constants.DEFAULT_SPEED_PERCENT_AVATAR;

            // IsDisplayGrid
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_DISPLAY_GRID] == "True")
                IsDisplayGrid = true;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_DISPLAY_GRID] == "False")
                IsDisplayGrid = false;

            // IsTraced
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_TRACED] == "True")
                IsTraced = true;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_TRACED] == "False")
                IsTraced = false;

            // IsTracedPerformance
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_TRACED_PERFORMANCE] == "True")
                IsTracedPerformance = true;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_TRACED_PERFORMANCE] == "False")
                IsTracedPerformance = false;

            // IsDisplayAxe
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_DISPLAY_AXE] == "True")
                IsDisplayAxe = true;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_DISPLAY_AXE] == "False")
                IsDisplayAxe = false;

            // IsToric
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_TORIC] == "True")
                IsToric = true;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_TORIC] == "False")
                IsToric = false;

            // IsMoore
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_MOORE] == "True")
                IsMoore = true;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_IS_MOORE] == "False")
                IsMoore = false;

            // SchedulingStrategy
            if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Fair.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Fair;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Sequential.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Sequential;
            else if (ConfigurationManager.AppSettings[Constants.APP_CONFIG_KEY_SCHEDULING_STRATEGY] == SchedulingStrategyEnum.Random.ToString())
                SchedulingStrategy = SchedulingStrategyEnum.Random;

            // Seed
            if (Seed == 0)
                Random = new Random(System.Guid.NewGuid().GetHashCode());
            else
                Random = new Random(Seed);
        }

        #endregion
    }
}