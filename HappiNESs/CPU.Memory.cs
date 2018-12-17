using System;
using static HappiNESs.CPU.AddressingModes;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        #region Public Properties

        /// <summary>
        /// The current access memory address
        /// </summary>
        public uint? CurrentMemoryAddress { get; set; }

        /// <summary>
        /// The current read memory value
        /// </summary>
        public uint CurrentMemoryValue { get; set; }

        #endregion

        #region Enums Types

        /// <summary>
        /// All the addressing mode in the processor
        /// </summary>
        public enum AddressingModes
        {
            None,
            Direct,
            Indirect,
            IndirectX,
            IndirectY,
            Immediate,
            ZeroPage,
            ZeroPageX,
            ZeroPageY,
            Absolute,
            AbsoluteX,
            AbsoluteY,
        }

        #endregion

        #region Constructor

        protected override void InitializeMemoryMap()
        {
            base.InitializeMemoryMap();

            // Generate Mappers
            MapReadHandler(0x0000, 0x1FFF, addr => Ram[addr & 0x07FF]);

            MapWriteHandler(0x0000, 0x1FFF, (addr, value) => Ram[addr & 0x07FF] = value);

            // TODO: Mapper initialization
        }

        #endregion

        #region Memory Methods

        private uint ParseMemoryAddress()
        {
            var def = OpcodeDefinitions[CurrentOpcode];
            switch (def.Mode)
            {
                case Immediate:
                    return PC++;
                case ZeroPage:
                    return NextByte();
                case ZeroPageX:
                    return (NextByte() + X) & 0xFF;
                case ZeroPageY:
                    return (NextByte() + Y) & 0xFF;
                case Absolute:
                    return NextWord();

            }

            // If no address mode found
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a value from the memory, making the addressing mode stuff
        /// </summary>
        /// <returns></returns>
        public uint AddressRead()
        {
            // If its Direct mode, just return A
            if (OpcodeDefinitions[CurrentOpcode].Mode == AddressingModes.Direct)
                return CurrentMemoryValue = A;

            // Get the address
            if (CurrentMemoryAddress == null)
                CurrentMemoryAddress = ParseMemoryAddress();

            // Return the value
            return CurrentMemoryValue = ReadByte((uint)CurrentMemoryAddress) & 0xFF;
        }

        /// <summary>
        /// Writes a value to the memory, making the addressing mode stuff
        /// </summary>
        /// <returns></returns>
        public void AddressWrite(uint Value)
        {
            // If its Direct mode, just writes on A
            if (OpcodeDefinitions[CurrentOpcode].Mode == AddressingModes.Direct)
                A = Value;
            else
            {
                // Get the address
                if (CurrentMemoryAddress == null)
                    CurrentMemoryAddress = ParseMemoryAddress();

                // Writes the value
                WriteByte((uint)CurrentMemoryAddress, Value);
            }
        }

        /// <summary>
        /// Reads the next instruction byte
        /// </summary>
        /// <returns></returns>
        public uint NextByte() => ReadByte(PC++);

        /// <summary>
        /// Reads the two next instruction bytes
        /// </summary>
        /// <returns></returns>
        public uint NextWord() => NextByte() | (NextByte() << 8);

        #endregion
    }
}
