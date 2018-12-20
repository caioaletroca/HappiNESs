using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        #region Public Properties

        /// <summary>
        /// The current CPU state
        /// </summary>
        public CPUState State { get; set; }

        #endregion

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

            State = GetState();

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

        /// <summary>
        /// Test the CPU with the nestest.nes
        /// </summary>
        /// <returns></returns>
        public bool CPUTest()
        {
            var Result = false;

            // Offset PC
            PC = 0xC000;

            // Get the logs
            var NestestLog = GetNestestLog("nestest.txt");

            var CurrentLine = 0;
            
            try
            {
                for (var i = 0; i < NestestLog.Count; i++)
                {   
                    // Run one cycle
                    ExecuteInstruction();

                    // Check if states is differents
                    if (NestestLog[CurrentLine] != State)
                        Debugger.Break();

                    // Increments counter
                    CurrentLine++;
                }

                Result = true;
            }
            catch (Exception e)
            {
                // Log
                IoC.Logger.Log(e.Message, LogLevel.Error);

                Result = false;
            }

            return Result;
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

        #region Test Methods

        /// <summary>
        /// Opens and loads the nestest.nes logs file
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns></returns>
        public List<CPUState> GetNestestLog(string path)
        {
            // Loads the Checking file
            var TestList = new List<CPUState>();
            using (var fileStream = new FileStream("nestest.txt", FileMode.Open))
            using (var stream = new StreamReader(fileStream))
            {
                var line = "";
                while ((line = stream.ReadLine()) != null)
                {
                    TestList.Add(new CPUState()
                    {
                        PC = line.Substring(0, 4).ToUint(),
                        //Opcode = Convert.ToUInt16(line.Substring(0, 4)),
                        A = line.Substring(line.IndexOf("A:") + 2, 2).ToUint(),
                        X = line.Substring(line.IndexOf("X:") + 2, 2).ToUint(),
                        Y = line.Substring(line.IndexOf("Y:") + 2, 2).ToUint(),
                        P = line.Substring(line.IndexOf("P:") + 2, 2).ToUint(),
                        SP = line.Substring(line.IndexOf("SP:") + 3, 2).ToUint(),
                    });
                }
            }

            return TestList;
        }

        #endregion

        #region Helpers Methods

        /// <summary>
        /// Get the current CPU state as <see cref="CPUState"/>
        /// </summary>
        /// <returns></returns>
        public CPUState GetState()
        {
            return new CPUState()
            {
                PC = PC - 1,
                Opcode = CurrentOpcode,
                A = A,
                X = X,
                Y = Y,
                P = P,
                SP = SP,
            };
        }

        #endregion
    }
}
