using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        #region Private Properties

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

            using (var fileStream = new FileStream("nestest.txt", FileMode.Open))
            using (var stream = new StreamReader(fileStream))
            {
                var line = "";
                while((line = stream.ReadLine()) != null)
                {
                    TestPC.Add(line.Substring(0, line.IndexOf(" ")));
                    TestA.Add(line.Substring(line.IndexOf("A:") + 2, 2));
                    TestX.Add(line.Substring(line.IndexOf("X:") + 2, 2));
                    TestY.Add(line.Substring(line.IndexOf("Y:") + 2, 2));
                    TestP.Add(line.Substring(line.IndexOf("P:") + 2, 2));
                    TestSP.Add(line.Substring(line.IndexOf("SP:") + 3, 2));
                }
            }
        }

        #endregion

        #region Run Methods

        public int CurrentLine = 0;

        public List<string> TestPC { get; set; } = new List<string>();
        public List<string> TestA { get; set; } = new List<string>();
        public List<string> TestX { get; set; } = new List<string>();
        public List<string> TestY { get; set; } = new List<string>();
        public List<string> TestP { get; set; } = new List<string>();
        public List<string> TestSP { get; set; } = new List<string>();

        /// <summary>
        /// Runs one instruction read by memory
        /// </summary>
        public void ExecuteInstruction()
        {
            // Handle interruptions
            HandleInterruptions();
            
            // Get the new opcode
            CurrentOpcode = NextByte();

            Console.Write($"{(PC - 1).ToString("X4")} ");
            Console.Write($"{CurrentOpcode.ToString("X2")}\t");
            Console.Write($"A: {A.ToString("X2")} ");
            Console.Write($"X: {X.ToString("X2")} ");
            Console.Write($"Y: {Y.ToString("X2")} ");
            Console.Write($"P: {P.ToString("X2")} ");
            Console.Write($"SP: {SP.ToString("X2")} ");
            Console.Write($"CYC: {Cycle.ToString("X")}\n");

            if (
                (PC - 1).ToString("X4") != TestPC[CurrentLine] ||
                A.ToString("X2") != TestA[CurrentLine] ||
                X.ToString("X2") != TestX[CurrentLine] ||
                Y.ToString("X2") != TestY[CurrentLine] ||
                P.ToString("X2") != TestP[CurrentLine] ||
                SP.ToString("X2") != TestSP[CurrentLine]
            )
                Debugger.Break();

            if ((PC - 1) == 0x0000)
                Debugger.Break();

            var opcode = Opcodes[CurrentOpcode];
            if (opcode == null)
                throw new ArgumentException();

            CurrentLine++;

            // Update Cycles
            Cycle += OpcodeDefinitions[CurrentOpcode].Cycles;

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
