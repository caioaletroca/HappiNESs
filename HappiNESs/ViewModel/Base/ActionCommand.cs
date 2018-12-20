using System;
using System.Windows.Input;

namespace HappiNESs
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    public class ActionCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action _Action;

        #endregion

        #region Public Events

        /// <summary>
        /// The event thats fires when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public ActionCommand(Action action)
        {
            _Action = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// A action command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) {
            return true;
        }

        /// <summary>
        /// Execute the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _Action();
        }

        #endregion
    }
}
