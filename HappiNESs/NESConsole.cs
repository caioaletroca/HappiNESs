namespace HappiNESs
{
    /// <summary>
    /// Handles and contains all the NES emulation components
    /// </summary>
    internal class NESConsole
    {
        #region Public Properties

        /// <summary>
        /// The CPU core of a 6502 processor
        /// </summary>
        public readonly CPU CPU;

        /// <summary>
        /// The current loaded ROM
        /// </summary>
        public Cartridge Cartridge { get; set; }

        public BaseMappers Mapper { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public NESConsole()
        {
            CPU = new CPU(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads a ROM from a file in INES format
        /// </summary>
        /// <param name="path">The path to the file</param>
        public void LoadRom(string path)
        {
            Cartridge = new Cartridge(path);
            Mapper = new NROM(this);
        }

        public void SaveState()
        {

        }

        public void LoadState()
        {

        }

        #endregion
    }
}
