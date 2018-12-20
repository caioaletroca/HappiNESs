using System.Windows.Input;
using System.Xml.Serialization;

namespace HappiNESs
{
    /// <summary>
    /// A base view model for any dialogs
    /// </summary>
    public class BaseDialogViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The title of the dialog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Defines the dialog TopMost property. Defaults to <see cref="True"/>
        /// </summary>
        public bool TopMost { get; set; } = true;

        #endregion

        #region Commands

        /// <summary>
        /// Command to OK button on dialog window
        /// </summary>
        [XmlIgnore]
        public ICommand OKCommand { get; set; }

        /// <summary>
        /// Command to yes button on dialog window
        /// </summary>
        [XmlIgnore]
        public ICommand YesCommand { get; set; }

        /// <summary>
        /// Command to no button on dialog window
        /// </summary>
        [XmlIgnore]
        public ICommand NoCommand { get; set; }

        /// <summary>
        /// Command to close the current window
        /// </summary>
        [XmlIgnore]
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Command to cancel the dialog window
        /// </summary>
        [XmlIgnore]
        public ICommand CancelCommand { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// The Close Event fires when the Dialog Window will be closed
        /// </summary>
        public event DialogEventHandler CloseEvent;

        #endregion

        #region Delegates

        /// <summary>
        /// The event handler for <see cref="BaseDialogViewModel"/>
        /// </summary>
        public delegate void DialogEventHandler();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDialogViewModel()
        {
            // Bind commands
            OKCommand = new ActionCommand(() => OK());
            YesCommand = new ActionCommand(() => Yes());
            NoCommand = new ActionCommand(() => No());
            CancelCommand = new ActionCommand(() => Cancel());
            CloseCommand = new ActionCommand(() => CloseWindow());
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Fires when the user press the OK button
        /// </summary>
        public virtual void OK() { }

        /// <summary>
        /// Fires when the user press the Yes button
        /// </summary>
        public virtual void Yes() { }

        /// <summary>
        /// Fires when the user press the No Button
        /// </summary>
        public virtual void No() { }

        /// <summary>
        /// Fires when the user press the Cancel
        /// </summary>
        public virtual void Cancel() { }

        /// <summary>
        /// Closes the current dialog window
        /// </summary>
        public virtual void CloseWindow()
        {
            CloseEvent?.Invoke();
        }

        #endregion
    }
}
