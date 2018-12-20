using System;
using System.Runtime.CompilerServices;

namespace HappiNESs
{
    /// <summary>
    /// Holds a bunch of loggers to log messages for the user
    /// </summary>
    public interface ILogFactory
    {
        #region Properties

        /// <summary>
        /// The level of logging to output
        /// </summary>
        LogOutputLevel LogOutputLevel { get; set; }

        /// <summary>
        /// if true, includes the origin of where the log message was logged from
        /// such as the class name, line number and file name
        /// </summary>
        bool IncludeLogOriginDetails { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Fires whenever a new log arrives
        /// </summary>
        event Action<(string Message, LogLevel Level, string Channel)> NewLog;

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specific logger to this factory
        /// </summary>
        /// <param name="logger"></param>
        void AddLogger(ILogger logger);

        /// <summary>
        /// Removes the specified logger from this factory
        /// </summary>
        /// <param name="logger"></param>
        void RemoveLogger(ILogger logger);

        /// <summary>
        /// Stops and removes the main file logger <see cref="MainFileLogger"/> from system
        /// </summary>
        void RemoveFileLogger();

        /// <summary>
        /// Logs the specific message to all loggers
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message being logged</param>
        /// <param name="origin">The method/function this message war logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        void Log(string message, LogLevel level = LogLevel.Informative, string channel = "", [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        #endregion
    }
}
