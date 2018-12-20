﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace HappiNESs
{
    /// <summary>
    /// The standard log factory
    /// Logs details to the Debug by default
    /// </summary>
    public class BaseLogFactory : ILogFactory
    {
        #region Protected Methods

        /// <summary>
        /// The list of loggers in this factory
        /// </summary>
        protected List<ILogger> mLoggers = new List<ILogger>();

        /// <summary>
        /// A lock for the logger list to keep it thread-safe
        /// </summary>
        protected object mLoggersLock = new object();

        /// <summary>
        /// A reference to the main file logger as <see cref="ILogger"/>
        /// </summary>
        protected ILogger MainFileLogger { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// The level of logging to output
        /// </summary>
        public LogOutputLevel LogOutputLevel { get; set; }

        /// <summary>
        /// if true, includes the origin of where the log message was logged from
        /// such as the class name, line number and file name
        /// </summary>
        public bool IncludeLogOriginDetails { get; set; } = true;

        #endregion

        #region Public Events

        /// <summary>
        /// Fires whenever a new log arrives
        /// </summary>
        public event Action<(string Message, LogLevel Level, string Channel)> NewLog = (details) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="loggers">The loggers to add to the factory, on top of the stock loggers already included</param>
        public BaseLogFactory(ILogger[] loggers = null)
        {
            // Add console logger
            AddLogger(new DebugLogger());
            AddLogger(new ConsoleLogger());

            // Add any others passed in
            if (loggers != null)
                foreach (var logger in loggers)
                    AddLogger(logger);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specific logger to this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public virtual void AddLogger(ILogger logger)
        {
            // Lock the list so it is thread-safe
            lock (mLoggersLock)
            {
                // If the logger is not already in the list...
                if (!mLoggers.Contains(logger))
                {
                    // Add the logger to the list
                    mLoggers.Add(logger);

                    // Save reference to the main file logger so that we can kill it
                    if (logger is FileLogger)
                        MainFileLogger = logger;
                }
                    
            }
        }

        /// <summary>
        /// Removes the specified logger from this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public virtual void RemoveLogger(ILogger logger)
        {
            // Lock the list so it is thread-safe
            lock (mLoggersLock)
            {
                // If the logger is in the list...
                if (mLoggers.Contains(logger))
                    // Remove the logger to the list
                    mLoggers.Remove(logger);
            }
        }

        /// <summary>
        /// Stops and removes the main file logger <see cref="MainFileLogger"/> from system
        /// </summary>
        public virtual void RemoveFileLogger()
        {
            RemoveLogger(MainFileLogger);
        }

        /// <summary>
        /// Logs the specific message to all loggers
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message being logged</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        public virtual void Log(string message, LogLevel level = LogLevel.Informative, string channel = "", [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            // If we should not log the message as the level is too low...
            if ((int)level < (int)LogOutputLevel)
                return;

            // If the user wants to know where the log originated from...
            if (IncludeLogOriginDetails)
                message = $"{message} [{Path.GetFileName(filePath)} > {origin} > Line {lineNumber}]";

            // Lock the list to it is thread-safe
            lock (mLoggersLock)
            {
                // Log to all loggers
                mLoggers.ForEach(logger => logger.Log(message, level));
            }

            // Inform listeners
            NewLog.Invoke((message, level, channel));
        }

        #endregion
    }
}
