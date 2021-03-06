﻿using System;
using System.Threading.Tasks;

namespace HappiNESs
{
    /// <summary>
    /// The view model for the application
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// Instance of the emulator
        /// </summary>
        private NESConsole NES { get; set; } = new NESConsole();

        #endregion

        #region Events

        /// <summary>
        /// Fired when the user closes the entire application as <see cref="ApplicationEvents"/>
        /// </summary>
        public event ApplicationEvents OnApplicationClose;

        /// <summary>
        /// Triggers to closes the application
        /// </summary>
        public event ApplicationEvents ApplicationClose;

        #endregion

        #region Delegates

        /// <summary>
        /// The application type of events
        /// </summary>
        public delegate void ApplicationEvents();

        #endregion

        #region NES Methods

        /// <summary>
        /// Loads a rom from a file
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns></returns>
        public async Task LoadRomAsync(string path)
        {
            try
            {
                // Load the rom
                NES.LoadRom(path);

                // TODO: Fix nes initialization
                NES.CPU.Initialize();

                // Log
                IoC.Logger.Log($"The rom {path} was loaded.", LogLevel.Success);
            }
            catch (UnauthorizedAccessException)
            {
                // Log
                IoC.Logger.Log($"The program was unauthorized to open the file {path}", LogLevel.Error);
            }
        }

        /// <summary>
        /// Test the <see cref="CPU"/> if all is running correctly
        /// </summary>
        /// <returns></returns>
        public async Task CPUTestAsync()
        {
            // Log
            IoC.Logger.Log("The CPU test has started");

            // Run test
            if (NES.CPUTest())
                IoC.Logger.Log("CPU is running successful.", LogLevel.Success);
            else
                IoC.Logger.Log("A error occurred in CPU.", LogLevel.Error);
        }

        #endregion
    }
}
