namespace HappiNESs
{
    /// <summary>
    /// The Main core for the CPU 6502
    /// </summary>
    sealed partial class CPU
    {
        #region Public Properties

        public int Cycle;

        #endregion

        #region Delegates

        public delegate void Opcode();

        #endregion

        #region Private Properties

        private readonly Opcode[] Opcodes = new Opcode[256];

        #endregion
    }
}
