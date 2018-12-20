using System.Windows;
using System.Windows.Controls;

namespace HappiNESs
{
    /// <summary>
    /// The view model for a dialog window
    /// </summary>
    public class DialogWindowViewModel : WindowViewModel
    {
        #region Public Properties

        /// <summary>
        /// The title for this dialog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content to host inside the dialog
        /// </summary>
        public Control Content { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="window"></param>
        public DialogWindowViewModel(Window window) : base(window)
        {

        }

        #endregion
    }
}
