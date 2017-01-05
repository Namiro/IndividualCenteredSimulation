namespace IndividualCenteredSimulation.Constants
{
    /// <summary>
    /// To make a constants : public const TYPE XXX = VALUE
    /// OR
    /// To make a constants : public static readonly TYPE XXX = VALUE
    /// </summary>
    public static class Constants
    {
        // Divers


        // Default values
        public const int DEFAULT_GRID_SIZE_X = 25;
        public const int DEFAULT_GRID_SIZE_Y = 25;
        public const int DEFAULT_CANVAS_SIZE_X = 500;
        public const int DEFAULT_CANVAS_SIZE_Y = 500;
        public const int DEFAULT_BOX_SIZE = 20;
        public const long DEFAULT_DELAY_MILLISECONDE = 1000;
        public const long DEFAULT_TICK_NUMBER = 100;
        public const bool DEFAULT_IS_DISPLAY_GRID = true;
        public const bool DEFAULT_IS_TRACED = false;
        public const int DEFAULT_SEED = 0;
        public const int DEFAULT_RATE_REFRESH = 1;
        public const SchedulingStrategyEnum DEFAULT_SCHEDULING_STRATEGY = SchedulingStrategyEnum.Fair;


        // Keys of App.config
        public const string APP_CONFIG_KEY_GRID_SIZE_X = "GridSizeX";
        public const string APP_CONFIG_KEY_GRID_SIZE_Y = "GridSizeY";
        public const string APP_CONFIG_KEY_CANVAS_SIZE_X = "CanvasSizeX";
        public const string APP_CONFIG_KEY_CANVAS_SIZE_Y = "CanvasSizeY";
        public const string APP_CONFIG_KEY_BOX_SIZE = "BoxSize";
        public const string APP_CONFIG_KEY_DELAY_MILLISECONDE = "DelayMilliseconde";
        public const string APP_CONFIG_KEY_TICKS_NUMBER = "TicksNumber";
        public const string APP_CONFIG_KEY_IS_DISPLAY_GRID = "IsDisplayGrid";
        public const string APP_CONFIG_KEY_IS_TRACED = "IsTraced";
        public const string APP_CONFIG_KEY_SEED = "Seed";
        public const string APP_CONFIG_KEY_RATE_REFRESH = "RateRefresh";
        public const string APP_CONFIG_KEY_SCHEDULING_STRATEGY = "SchedulingStrategy";


        // Keys for the execution App Properties

    }
}
