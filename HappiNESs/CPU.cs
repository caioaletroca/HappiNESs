namespace HappiNESs
{
    /// <summary>
    /// The Main core for the CPU 6502
    /// </summary>
    internal sealed partial class CPU
    {
        #region Public Properties

        public int Cycle;

        public byte[] RAM = new byte[0x800];

        #endregion

        #region Delegates

        public delegate void Opcode();

        #endregion

        #region Private Properties

        private readonly Opcode[] Opcodes = new Opcode[256];

        #endregion
    }
}
