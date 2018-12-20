using System;
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

        #region Public Methods

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

        #endregion
    }
}
