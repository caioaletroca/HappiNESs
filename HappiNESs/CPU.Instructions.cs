using System;
using static HappiNESs.CPU.AddressingModes;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
        public class OpcodeDefinition : Attribute
        {
            /// <summary>
            /// The opcode value
            /// </summary>
            public int Opcode { get; set; }

            /// <summary>
            /// The addressing mode for the opcode
            /// </summary>
            public AddressingModes Mode { get; set; } = AddressingModes.None;

            /// <summary>
            /// The number of cycles spend on the opcode
            /// </summary>
            public int Cycles { get; set; } = 2;

            /// <summary>
            /// If the opcode have page boundary behavior
            /// </summary>
            public bool PageBoundary { get; set; }
        }

        #region Storage Instructions

        [OpcodeDefinition(Opcode = 0xA9, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xA5, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xB5, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xAD, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xBD, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xB9, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xA1, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xB1, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void LDA() => A = AddressRead();

        [OpcodeDefinition(Opcode = 0xA2, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xA6, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xB6, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xAE, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xBE, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        private void LDX() => X = AddressRead();

        [OpcodeDefinition(Opcode = 0xA0, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xA4, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xB4, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xAC, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xBC, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        private void LDY() => Y = AddressRead();

        [OpcodeDefinition(Opcode = 0x85, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x95, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x8D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x9D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x99, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x81, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x91, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void STA() { }

        [OpcodeDefinition(Opcode = 0x86, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x96, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x8E, Mode = Absolute, Cycles = 4)]
        private void STX() { }

        [OpcodeDefinition(Opcode = 0x84, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x94, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x8C, Mode = Absolute, Cycles = 4)]
        private void STY() { }

        [OpcodeDefinition(Opcode = 0xAA)]
        private void TAX() => X = A;

        [OpcodeDefinition(Opcode = 0xA8)]
        private void TAY() => Y = A;

        [OpcodeDefinition(Opcode = 0xBA)]
        private void TSX() => X = SP;

        [OpcodeDefinition(Opcode = 0x8A)]
        private void TXA() => A = X;

        [OpcodeDefinition(Opcode = 0x9A)]
        private void TXS() => SP = X;

        [OpcodeDefinition(Opcode = 0x98)]
        private void TYA() => A = Y;

        #endregion

        #region Math Instructions

        [OpcodeDefinition(Opcode = 0x69, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x65, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x75, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x6D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x7D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x79, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x61, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x71, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void ADC()
        {
            // Reads the M
            var Value = AddressRead();

            // Sum with overflow
            var newValue = (sbyte)A + (sbyte)Value + (sbyte)(Flags.Carry ? 1 : 0);

            // Set flags
            Flags.Overflow = newValue < -128 || newValue > 127;
            Flags.Carry = A + Value + (Flags.Carry ? 1 : 0) > 0xFF;

            // Set Accumulator
            A = (byte)(newValue & 0xFF);
        }

        [OpcodeDefinition(Opcode = 0xC6, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0xD6, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xCE, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xDE, Mode = AbsoluteX, Cycles = 7)]
        private void DEC()
        {
            
        }


        private void DEX()
        {

        }

        #endregion

        #region Bitwise Instructions

        /// <summary>
        /// ANDS M and Accumulator
        /// </summary>
        [OpcodeDefinition(Opcode = 0x29)]
        [OpcodeDefinition(Opcode = 0x25, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x35, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x2D, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x3D, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x39, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x21, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x31, Cycles = 5)]
        private void AND() => A &= Operand;

        /// <summary>
        /// Exclusive OR M and Accumulator
        /// </summary>
        private void EOR() => A ^= Operand;

        /// <summary>
        /// ORs M and Accumulator
        /// </summary>
        private void ORA() => A |= Operand;

        #endregion
    }
}
