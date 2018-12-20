namespace HappiNESs
{
    /// <summary>
    /// The view model for the application
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
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
    }
}
