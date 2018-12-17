using System;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        #region Constructor

        /// <summary>
        /// Resets all the registers
        /// </summary>
        private void Initialize()
        {
            A = 0;
            X = 0;
            Y = 0;
        }

        #endregion

        #region Run Methods

        /// <summary>
        /// Runs one instruction read by memory
        /// </summary>
        public void ExecuteInstruction()
        {
            // Get the new opcode
            CurrentOpcode = NextByte();
            var opcode = Opcodes[CurrentOpcode];
            if (opcode == null)
                throw new ArgumentException();

            // Update Cycles
            Cycle += OpcodeDefinitions[CurrentOpcode].Cycles;

            CurrentMemoryAddress = null;

            // Run opcode
            opcode();
        }

        #endregion
    }
}
