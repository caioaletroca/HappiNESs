using System;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        #region Private Properties

        /// <summary>
        /// Control flags for interruptions
        /// </summary>
        private readonly bool[] Interrupts = new bool[2];

        /// <summary>
        /// The interruptions memory vectors
        /// </summary>
        private readonly uint[] InterruptsOffset =
        {
            0xFFFA, 0xFFFE, 0xFFFC,
        };

        #endregion

        #region Enum Definitions

        /// <summary>
        /// All types of CPU interruptions
        /// </summary>
        public enum InterruptTypes
        {
            NMI,
            IRQ,
            RESET,
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Resets all the registers
        /// </summary>
        public void Initialize()
        {
            // Start conditions
            A = 0;
            X = 0;
            Y = 0;

            SP = 0xFD;
            P = 0x24;

            // Loads Startup interruptions
            PC = ReadWord(InterruptsOffset[(int)InterruptTypes.RESET]);
        }

        #endregion

        #region Run Methods

        /// <summary>
        /// Runs one instruction read by memory
        /// </summary>
        public void ExecuteInstruction()
        {
            // Handle interruptions
            HandleInterruptions();
            
            // Get the new opcode
            CurrentOpcode = NextByte();
            var opcode = Opcodes[CurrentOpcode];
            if (opcode == null)
                throw new ArgumentException();

            // Update Cycles
            Cycle += OpcodeDefinitions[CurrentOpcode].Cycles;

            // Clear
            CurrentMemoryAddress = null;

            // Run opcode
            opcode();
        }

        #endregion

        #region Interruptions Methods

        /// <summary>
        /// Runs and handles interruptions
        /// </summary>
        public void HandleInterruptions()
        {
            for (var i = 0; i < Interrupts.Length; i++)
            {
                if (Interrupts[i])
                {
                    // Push to the stack
                    PushWord(PC);
                    Push(P);

                    // Read memory vector
                    PC = ReadWord(InterruptsOffset[i]);

                    // Update flags
                    Flags.InterruptDisable = true;
                    Interrupts[i] = false;

                    return;
                }
            }
        }

        /// <summary>
        /// Runs when a interruption of Reset type is triggered
        /// </summary>
        private void Reset()
        {
            SP -= 3;
            Flags.InterruptDisable = true;
        }

        /// <summary>
        /// Triggers a specific interruption type
        /// </summary>
        /// <param name="Type"></param>
        public void TriggerInterrupt(InterruptTypes Type)
        {
            if (!Flags.InterruptDisable || Type == InterruptTypes.NMI)
                Interrupts[(int)Type] = true;
        }

        #endregion
    }
}
