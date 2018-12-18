namespace HappiNESs
{
    internal abstract class BaseMappers
    {
        #region Protected Properties

        protected readonly byte[] PRGROM;
        protected readonly byte[] PRGRAM = new byte[0x2000];
        protected readonly byte[] CHRROM;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseMappers(NESConsole console)
        {
            PRGROM = console.Cartridge.PRGROM;
            CHRROM = console.Cartridge.CHRROM;
        }

        #endregion

        public virtual void InitializeMemoryMap(CPU cpu)
        {

        }
    }
}
