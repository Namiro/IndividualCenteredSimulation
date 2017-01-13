using log4net;
using log4net.Config;
using System;

[assembly: XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
namespace MultiAgentSystem.Helpers
{
    /// <summary>
    /// This class allow to use Log4NET.
    /// Each Logger available correspond to a file.
    /// </summary>
    internal static class Logger
    {
        #region Constants
        //Créer loggers : ils doivent être déclaré ici et dans le fichier Log4NET.config
        //Attention les string qui sont ici doivent correspondre à ceux du nom des loggers correspondant dans la configuration
        public static readonly ILog CONSOLE = LogManager.GetLogger("Console");
        #endregion

        #region Constructors
        static Logger()
        {
            XmlConfigurator.Configure();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Write a log in the console with the level : Debug
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void WriteLog(String message)
        {
            WriteLog(CONSOLE, message, LogLevelL4N.DEBUG);

        }

        /// <summary>
        /// Write a log in the console
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="logLevel">The log level</param>
        public static void WriteLog(String message, LogLevelL4N logLevel)
        {
            WriteLog(CONSOLE, message, logLevel);
        }

        /// <summary>
        /// Write a log
        /// </summary>
        /// <param name="logger">One of the logger available in the config file and declared in this class</param>
        /// <param name="message">The message to log</param>
        /// <param name="logLevel">The log level</param>
        public static void WriteLog(ILog logger, String message, LogLevelL4N logLevel)
        {
            switch (logLevel)
            {
                case LogLevelL4N.DEBUG:
                    logger.Debug(message);
                    break;
                case LogLevelL4N.ERROR:
                    logger.Error(message);
                    break;
                case LogLevelL4N.FATAL:
                    logger.Fatal(message);
                    break;
                case LogLevelL4N.INFO:
                    logger.Info(message);
                    break;
                case LogLevelL4N.WARN:
                    logger.Warn(message);
                    break;
            }
        }
        #endregion
    }


    /// <summary>
    /// Each enum correspon at one enum in Log4NET
    /// </summary>
    internal enum LogLevelL4N
    {
        DEBUG = 1,
        ERROR,
        FATAL,
        INFO,
        WARN
    }
}