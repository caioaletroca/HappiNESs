namespace HappiNESs
{
    /// <summary>
    /// The Standard Message Box view model
    /// </summary>
    public class MessageBoxViewModel : BaseDialogViewModel
    {
        #region Public Properties

        /// <summary>
        /// The message to be displayed
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A flag if Yes button is visible
        /// </summary>
        public bool YesButton { get; set; }

        /// <summary>
        /// A flag if No button is visible
        /// </summary>
        public bool NoButton { get; set; }

        /// <summary>
        /// A flag if OK button is visible
        /// </summary>
        public bool OKButton { get; set; }

        /// <summary>
        /// A flag if Cancel button is visible
        /// </summary>
        public bool CancelButton { get; set; }

        /// <summary>
        /// The user input response for this dialog
        /// </summary>
        public int Result { get; set; }

        #endregion

        #region Action Methods

        public override void Yes()
        {
            // Save the response
            Result = (int)DialogResultType.Yes;

            // Closes the dialog
            CloseWindow();
        }

        public override void No()
        {
            // Save the response
            Result = (int)DialogResultType.No;

            // Closes the dialog
            CloseWindow();
        }

        public override void OK()
        {
            // Save the response
            Result = (int)DialogResultType.OK;

            // Closes the dialog
            CloseWindow();
        }

        public override void Cancel()
        {
            // Save the response
            Result = (int)DialogResultType.Cancel;

            // Closes the dialog
            CloseWindow();
        }

        #endregion
    }
}
