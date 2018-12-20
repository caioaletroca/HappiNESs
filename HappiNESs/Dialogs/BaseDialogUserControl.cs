using HappiNESs;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HappiNESs
{
    /// <summary>
    /// A base User Control for Dialog Windows
    /// </summary>
    public class BaseDialogUserControl : UserControl
    {
        #region Private Properties

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private DialogWindow mWindow;

        #endregion

        #region Public Properties

        /// <summary>
        /// The title for this dialog
        /// </summary>
        public string Title { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public BaseDialogUserControl()
        {
            if(!DesignerProperties.GetIsInDesignMode(this))
            {
                //Crete a new dialog window
                mWindow = new DialogWindow();
                mWindow.ViewModel = new DialogWindowViewModel(mWindow);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Show the window and stops the application until the window is closed
        /// </summary>
        /// <param name="async">A flag that control if we are open a dialog or a window</param>
        /// <returns></returns>
        public async Task<int> ShowWindowAsync(bool async = false)
        {
            // Create a task to await the dialog closing
            var Result = 0;

            // Run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    // Set Dialog Title
                    mWindow.ViewModel.Title = Title;
                    
                    // Set this control to the dialog window content
                    mWindow.ViewModel.Content = this;

                    // Show Window
                    if (async)
                        mWindow.Show();
                    else
                        mWindow.ShowDialog();
                }
                finally
                {
                    // Let caller know we finished
                    Result = (int)DialogResultType.OK;
                }
            });

            return Result;
        }

        /// <summary>
        /// Show the window and stops the application until the window is closed
        /// </summary>
        /// <typeparam name="T">The view model generic type</typeparam>
        /// <param name="viewModel">The view model passed to the window</param>
        /// <param name="async">A flag that control if we are open a dialog or a window</param>
        /// <returns></returns>
        public async Task<int> ShowWindowAsync<T>(T viewModel, bool async = false)
            where T : BaseDialogViewModel
        {
            // Create a task to await the dialog closing
            var Result = 0;

            // Run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    // Set Dialog Title
                    mWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    // Set this control to the dialog window content
                    mWindow.ViewModel.Content = this;

                    // Setup this controls data context binding to the view model
                    DataContext = viewModel;

                    // Register Close Window Event on View Model
                    (DataContext as BaseDialogViewModel).CloseEvent += CloseWindow;

                    // Show Window
                    if (async)
                        mWindow.Show();
                    else
                        mWindow.ShowDialog();
                }
                finally
                {
                    // Let caller know we finished
                    if(DataContext is MessageBoxViewModel model)
                        Result = model.Result;
                }
            });

            return Result;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Closes the current dialog window and fire an event to the view model
        /// </summary>
        public void CloseWindow()
        {
            // Close the dialog window
            mWindow.Close();
        }

        #endregion
    }
}
