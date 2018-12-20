using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappiNESs;

namespace HappiNESs
{
    /// <summary>
    /// The application implementation of the <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        #region Public Methods

        /// <summary>
        /// Start the action in the UI Thread
        /// </summary>
        /// <param name="action">The action to be executed</param>
        public void UIThread(Action action, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            // Start the action
            System.Windows.Application.Current.Dispatcher.Invoke(action);
        }

        #endregion

        #region Dialog Methods

        /// <summary>
        /// Creates a instance of System Folder Browser Dialog Window
        /// </summary>
        /// <returns></returns>
        public SystemDialogResult FolderBrowserDialog()
        {
            // Create the new dialog
            var dialog = new FolderBrowserDialog();

            // Show dialog and get result
            var result = dialog.ShowDialog();

            // Return results as a SystemDialogResult instance
            return new SystemDialogResult()
            {
                SelectedPath = dialog.SelectedPath,
                Result = (int)result,
            };
        }

        /// <summary>
        /// Creates a instance of System Open File Dialog window
        /// </summary>
        /// <param name="Filter">The file filter</param>
        /// <returns></returns>
        public SystemDialogResult OpenFileDialog(string Filter = "")
        {
            // Create the new dialog
            var dialog = new OpenFileDialog
            {
                Filter = Filter,
                AddExtension = true,
            };

            // Show dialog and get result
            var result = dialog.ShowDialog();

            // If user canceled the action
            if (result == DialogResult.Cancel)
                return new SystemDialogResult()
                {
                    Result = (int)result,
                };

            // Return the results as a SystemDialogResult instance
            return new SystemDialogResult()
            {
                FileName = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\') + 1),
                FilePath = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')),
                Result = (int)result,
            };
        }

        /// <summary>
        /// Creates a instance of System Save File Dialog window
        /// </summary>
        /// <returns></returns>
        public SystemDialogResult SaveFileDialog(string Filter = "", string FileName = "")
        {
            // Create the new dialog
            var dialog = new SaveFileDialog
            {
                // Default configurations
                FileName = FileName,
                Filter = Filter,
                AddExtension = true,
                OverwritePrompt = true,
            };

            // Show dialog and get result
            var result = dialog.ShowDialog();

            // If user canceled the action
            if (result == DialogResult.Cancel)
                return new SystemDialogResult()
                {
                    Result = (int)result,
                };

            // Return results as a SystemDialogResult instance
            return new SystemDialogResult()
            {
                FileName = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\') + 1),
                FilePath = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')),
                Result = (int)result,
            };
        }

        #endregion

        #region Window Methods

        /// <summary>
        /// Displays a new window to the user
        /// <param name="dialog">The dialog window to be opened</param>
        /// <param name="async">A flag that control if we are openning a dialog or a window</param>
        /// </summary>
        public Task<int> ShowWindow(DialogWindowType dialog, bool async = false)
        {
            switch (dialog)
            {
                
                default:
                    // Show error dialog
                    ShowWindow(DialogWindowType.MessageBoxDialog, new MessageBoxViewModel()
                    {
                        Title = "Erro",
                        Message = "Erro ao tentar abrir uma janela de diálogo não esperada ou não existente.",
                    });
                    return new Task<int>(() => 0);
            }
        }

        /// <summary>
        /// Displays a new window to the user with added view model support
        /// </summary>
        /// <param name="dialog">The dialog window to be opened</param>
        /// <param name="viewModel">The view model attached to the dialog</param>
        /// <param name="async">A flag that control if we are openning a dialog or a window</param>
        /// <returns></returns>
        public Task<int> ShowWindow<T>(DialogWindowType dialog, T viewModel, bool async = false)
            where T : BaseDialogViewModel
        {
            switch (dialog)
            {

                default:
                    // Show error dialog
                    ShowWindow(DialogWindowType.MessageBoxDialog, new MessageBoxViewModel()
                    {
                        Title = "Erro",
                        Message = "Erro ao tentar abrir uma janela de diálogo não esperada ou não existente.",
                    });
                    return new Task<int>(() => 0);
            }
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Logs the given error to the log factory
        /// </summary>
        /// <param name="ex">The exception to log</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        private void LogError(Exception ex, string origin, string filePath, int lineNumber)
        {
            IoC.Logger.Log($"An unexpected error occurred running a IoC.Task.Run. {ex.Message}", LogLevel.Debug, "", origin, filePath, lineNumber);
        }

        #endregion
    }
}
