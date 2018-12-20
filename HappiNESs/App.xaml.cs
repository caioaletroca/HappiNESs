using System.Windows;

namespace HappiNESs
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region WPF Events

        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application
            ApplicationSetup();

            // Log it
            IoC.Logger.Log("Iniciando aplicação.", LogLevel.Informative);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configures application ready for use
        /// </summary>
        private void ApplicationSetup()
        {
            // Setup IoC
            IoC.Setup();

            // Bind a logger
            IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLogFactory());

            // Bind a task manager
            IoC.Kernel.Bind<ITaskManager>().ToConstant(new TaskManager());

            // Bind a ui manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());

            // Fire Start Event
            IoC.OnStart();
        }

        #endregion
    }
}
