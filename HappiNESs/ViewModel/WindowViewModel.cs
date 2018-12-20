using System.Windows;

namespace HappiNESs
{
    /// <summary>
    /// The view model for application main window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        #endregion

        #region Public Properties

        /// <summary>
        /// True if we should have a dimmed overlay on the window
        /// such as when a popup is visible or the window is not focused
        /// </summary>
        public bool DimmableOverlayVisible { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            // Register event to the Application Instance
            IoC.Application.ApplicationClose += CloseWindow;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Closes the application
        /// </summary>
        public void CloseWindow()
        {
            mWindow.Close();
        }

        #endregion
    }
}
