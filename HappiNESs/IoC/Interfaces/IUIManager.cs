using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HappiNESs
{
    /// <summary>
    /// The UI manager that handles any UI interaction in the application
    /// </summary>
    public interface IUIManager
    {
        #region Public Methods

        /// <summary>
        /// Start the action in the UI Thread
        /// </summary>
        /// <param name="action">The action to be executed</param>
        void UIThread(Action action, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        #endregion

        #region Dialog Methods

        /// <summary>
        /// Creates a instance of System Folder Browser Dialog Window
        /// </summary>
        /// <returns></returns>
        SystemDialogResult FolderBrowserDialog();

        /// <summary>
        /// Creates a instance of System Open File Dialog window
        /// </summary>
        /// <param name="Filter">The file filter</param>
        /// <returns></returns>
        SystemDialogResult OpenFileDialog(string Filter = "");

        /// <summary>
        /// Creates a instance of System Save File Dialog window
        /// </summary>
        /// <param name="Filter">The file filter</param>
        /// <returns></returns>
        SystemDialogResult SaveFileDialog(string Filter = "", string FileName = ApplicationConstants.DefaultExperimentFileName);

        #endregion

        /// <summary>
        /// Displays a new window to the user
        /// </summary>
        /// <param name="dialog">The dialog window to be opened</param>
        /// <param name="async">A flag that control if we are openning a dialog or a window</param>
        /// <returns></returns>
        Task<int> ShowWindow(DialogWindowType dialog, bool async = false);

        /// <summary>
        /// Displays a new window to the user with added view model support
        /// </summary>
        /// <param name="dialog">The dialog window to be opened</param>
        /// <param name="viewModel">The view model attached to the dialog</param>
        /// <param name="async">A flag that control if we are openning a dialog or a window</param>
        /// <returns></returns>
        Task<int> ShowWindow<T>(DialogWindowType dialog, T viewModel, bool async = false) where T : BaseDialogViewModel;
    }
}
