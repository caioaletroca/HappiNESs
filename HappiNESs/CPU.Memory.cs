using System;
using System.Runtime.CompilerServices;
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
            MapReadHandler(0x4000, 0x4017, ReadIORegister);

            MapWriteHandler(0x0000, 0x1FFF, (addr, value) => Ram[addr & 0x07FF] = value);
            MapWriteHandler(0x4000, 0x4017, WriteIORegister);
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
                case AbsoluteX:
                    var Address = NextWord();

                    // Handle page boundary
                    if (def.PageBoundary && (Address & 0xFF00) != ((Address + X) & 0xFF00))
                        Cycle++;
                    return Address + X;
                case AbsoluteY:
                    Address = NextWord();

                    // Handle page boundary
                    if (def.PageBoundary && (Address & 0xFF00) != ((Address + Y) & 0xFF00))
                        Cycle++;
                    return Address + Y;
                case IndirectX:
                    var Offset = (NextByte() + X) & 0xFF;
                    return ReadByte(Offset) | (ReadByte((Offset + 1) & 0xFF) << 8);
                case IndirectY:
                    Offset = NextByte() & 0xFF;
                    Address = ReadByte(Offset) | (ReadByte((Offset + 1) & 0xFF) << 8);

                    // Handle page boundary
                    if (def.PageBoundary && (Address & 0xFF00) != ((Address + Y) & 0xFF00))
                        Cycle++;
                    return (Address + Y) & 0xFFFF;
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

        #region Stack Methods

        /// <summary>
        /// Pushs a value to the stack
        /// </summary>
        /// <param name="Value">The value to insert</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Push(uint Value) => WriteByte(0x100 + (SP--), Value);

        /// <summary>
        /// Pops a value from the stack
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint Pop()
        {
            SP++;
            return ReadByte(0x100 + SP);
        }

        /// <summary>
        /// Pushs a word value to the stack
        /// </summary>
        /// <param name="Value">The value to insert</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PushWord(uint Value)
        {
            Push(Value >> 8);
            Push(Value & 0xFF);
        }

        /// <summary>
        /// Pops a word value from the stack
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint PopWord() => Pop() | (Pop() << 8);

        #endregion
    }
}
