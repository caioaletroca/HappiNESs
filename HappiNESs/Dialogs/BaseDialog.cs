using System.Windows;

namespace HappiNESs
{
    /// <summary>
    /// A base class for all Windows
    /// </summary>
    public class BaseDialog : Window
    {
        #region Private Member

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private DialogWindowViewModel mViewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        public DialogWindowViewModel ViewModel
        {
            get => mViewModel;
            set
            {
                // If nothing has changed, return
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Fire the view model changed method
                OnViewModelChanged();

                // Set the data context for this page
                DataContext = mViewModel;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when the view model changes
        /// </summary>
        protected virtual void OnViewModelChanged() { }

        #endregion
    }
}
