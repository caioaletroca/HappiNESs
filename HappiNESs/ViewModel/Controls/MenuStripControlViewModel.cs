using System.Threading.Tasks;
using System.Windows.Input;

namespace HappiNESs
{
    /// <summary>
    /// The view model for the menu strip
    /// </summary>
    public class MenuStripControlViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A flag that represents if the view model is busy
        /// </summary>
        public bool IsBusy { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Command to open a iNES rom
        /// </summary>
        public ICommand OpenRomCommand { get; set; }

        /// <summary>
        /// Command to execute a CPU test
        /// </summary>
        public ICommand TestCPUCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuStripControlViewModel()
        {
            // Bind commands
            OpenRomCommand = new ActionCommand(async () => await OpenRomAsync());
            TestCPUCommand = new ActionCommand(() => TestCPU());
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Opens a iNES rom
        /// </summary>
        /// <returns></returns>
        public async Task OpenRomAsync()
        {
            // Open file dialog
            var result = IoC.UI.OpenFileDialog(ApplicationConstants.NESFileFilter);

            // Return if user cancel
            if (result.Result != (int)DialogResultType.OK)
                return;
            
            // Run with control flag
            await RunCommandAsync(() => IsBusy, async () =>
            {
                // Run async
                await IoC.Task.Run(() =>
                {

                });
            });
        }

        /// <summary>
        /// Executes a CPU test
        /// </summary>
        public void TestCPU()
        {

        }

        #endregion
    }
}
