using Ninject;

namespace HappiNESs
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => IoC.Get<IUIManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ILogFactory"/>
        /// </summary>
        public static ILogFactory Logger => Get<ILogFactory>();

        /// <summary>
        /// A shortcut to access the <see cref="FileManager"/>
        /// </summary>
        public static FileManager File { get; private set; } = new FileManager();

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager Task => Get<ITaskManager>();

        /// <summary>
        /// A shortcut to access the <see cref="JsonManager"/>
        /// </summary>
        public static JsonManager Json { get; private set; } = new JsonManager();

        /// <summary>
        /// A shortcut to access the <see cref="XMLManager"/>
        /// </summary>
        public static XMLManager XML { get; private set; } = new XMLManager();

        /// <summary>
        /// A shortcut to access the <see cref="IServerManager"/>
        /// </summary>
        public static IServerManager User => Get<IServerManager>();

        /// <summary>
        /// A shortcut to access the <see cref="IStoreManager"/>
        /// </summary>
        public static IStoreManager Store => Get<IStoreManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => Get<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="SettingsViewModel"/>
        /// </summary>
        //public static SettingsViewModel Settings => Get<SettingsViewModel>();

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all 
        ///       services can be found
        /// </summary>
        public static void Setup()
        {
            // Bind all required view models
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels()
        {
            // Binds view models
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        #endregion

        #region WPF Events

        /// <summary>
        /// Fired when the IoC fully loaded all components
        /// </summary>
        public static void OnStart()
        {
            // Start Logging system
            Logger.AddLogger(new FileLogger("logfile.txt"));
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get's a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        #endregion
    }
}
