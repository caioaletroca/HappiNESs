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

        /// <summary>
        /// Load to Accumulator
        /// </summary>
        [OpcodeDefinition(Opcode = 0xA9, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xA5, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xB5, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xAD, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xBD, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xB9, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xA1, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xB1, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void LDA() => A = AddressRead();

        /// <summary>
        /// Load to X register
        /// </summary>
        [OpcodeDefinition(Opcode = 0xA2, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xA6, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xB6, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xAE, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xBE, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        private void LDX() => X = AddressRead();

        /// <summary>
        /// Load to Y register
        /// </summary>
        [OpcodeDefinition(Opcode = 0xA0, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xA4, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xB4, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xAC, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xBC, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        private void LDY() => Y = AddressRead();

        /// <summary>
        /// Store accumulator on address
        /// </summary>
        [OpcodeDefinition(Opcode = 0x85, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x95, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x8D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x9D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x99, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x81, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x91, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void STA() => AddressWrite(A);

        /// <summary>
        /// Store X register on address
        /// </summary>
        [OpcodeDefinition(Opcode = 0x86, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x96, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x8E, Mode = Absolute, Cycles = 4)]
        private void STX() => AddressWrite(X);

        /// <summary>
        /// Store Y register on address
        /// </summary>
        [OpcodeDefinition(Opcode = 0x84, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x94, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x8C, Mode = Absolute, Cycles = 4)]
        private void STY() => AddressWrite(Y);

        /// <summary>
        /// Transfer A to X
        /// </summary>
        [OpcodeDefinition(Opcode = 0xAA)]
        private void TAX() => X = A;

        /// <summary>
        /// Transfer A to Y
        /// </summary>
        [OpcodeDefinition(Opcode = 0xA8)]
        private void TAY() => Y = A;

        /// <summary>
        /// Transfer SP to X
        /// </summary>
        [OpcodeDefinition(Opcode = 0xBA, Cycles = 1)]
        private void TSX() => X = SP;

        /// <summary>
        /// Transfer X to A
        /// </summary>
        [OpcodeDefinition(Opcode = 0x8A)]
        private void TXA() => A = X;

        /// <summary>
        /// Transfer X to SP
        /// </summary>
        [OpcodeDefinition(Opcode = 0x9A, Cycles = 1)]
        private void TXS() => SP = X;

        /// <summary>
        /// Transfer Y to A
        /// </summary>
        [OpcodeDefinition(Opcode = 0x98)]
        private void TYA() => A = Y;

        #endregion

        #region Math Instructions

        /// <summary>
        /// Add Accumulator and M with carry
        /// </summary>
        [OpcodeDefinition(Opcode = 0x69, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x65, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x75, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x6D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x7D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x79, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x61, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x71, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void ADC() => ADCInstruction(AddressRead());

        /// <summary>
        /// Decrement M by one
        /// </summary>
        [OpcodeDefinition(Opcode = 0xC6, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0xD6, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xCE, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xDE, Mode = AbsoluteX, Cycles = 7)]
        private void DEC()
        {
            // Get the value
            var newValue = (byte)(AddressRead() - 1);

            HandleFlags(newValue);

            // Write the new value
            AddressWrite(newValue);
        }

        /// <summary>
        /// Decrement X by one
        /// </summary>
        [OpcodeDefinition(Opcode = 0xCA)]
        private void DEX() => X--;

        /// <summary>
        /// Decrement Y by one
        /// </summary>
        [OpcodeDefinition(Opcode = 0x88)]
        private void DEY() => Y--;

        /// <summary>
        /// Increment M by one
        /// </summary>
        [OpcodeDefinition(Opcode = 0xE6, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0xF6, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xEE, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xFE, Mode = AbsoluteX, Cycles = 7)]
        private void INC()
        {
            // Get the value
            var newValue = (byte)(AddressRead() + 1);

            HandleFlags(newValue);

            // Write the new value
            AddressWrite(newValue);
        }

        /// <summary>
        /// Increment X by one
        /// </summary>
        [OpcodeDefinition(Opcode = 0xE8)]
        private void INX() => X++;

        /// <summary>
        /// Increment Y by one
        /// </summary>
        [OpcodeDefinition(Opcode = 0xC8)]
        private void INY() => Y++;

        /// <summary>
        /// Subtract A and M with carry
        /// </summary>
        [OpcodeDefinition(Opcode = 0xE9, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xE5, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xF5, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xED, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xFD, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xF9, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xE1, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xF1, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void SBC() => ADCInstruction(~AddressRead());

        /// <summary>
        /// Implements only the sum part of the opcode
        /// </summary>
        /// <param name="Value">The value to sum</param>
        private void ADCInstruction(uint Value)
        {
            // Sum with overflow
            var newValue = (sbyte)A + (sbyte)Value + (sbyte)(Flags.Carry ? 1 : 0);

            // Set flags
            Flags.Overflow = newValue < -128 || newValue > 127;
            Flags.Carry = A + Value + (Flags.Carry ? 1 : 0) > 0xFF;

            // Set Accumulator
            A = (byte)(newValue & 0xFF);
        }

        #endregion

        #region Bitwise Instructions

        /// <summary>
        /// ANDS M and Accumulator
        /// </summary>
        [OpcodeDefinition(Opcode = 0x29, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x25, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x35, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x2D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x3D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x39, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x21, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x31, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void AND() => A &= AddressRead();

        /// <summary>
        /// Arithmetic shift left
        /// </summary>
        [OpcodeDefinition(Opcode = 0x0A, Mode = Direct)]
        [OpcodeDefinition(Opcode = 0x06, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0x16, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x0E, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x1E, Mode = AbsoluteX, Cycles = 7)]
        private void ASL()
        {
            // Get M value
            var Value = AddressRead();

            // Update Flags
            Flags.Carry = (Value & 0x80) > 0;

            Value <<= 1;

            HandleFlags(Value);

            // Write back to memory
            AddressWrite(Value);
        }

        /// <summary>
        /// Test Bits in M with A
        /// </summary>
        [OpcodeDefinition(Opcode = 0x24, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x2C, Mode = Absolute, Cycles = 4)]
        private void BIT()
        {
            // Get M value
            var newValue = AddressRead();

            // Check flags
            Flags.Zero = (newValue & A) == 0;
            Flags.Overflow = (newValue & 0x40) > 0;
            Flags.Negative = (newValue & 0x80) > 0;
        }

        /// <summary>
        /// Exclusive OR M and Accumulator
        /// </summary>
        [OpcodeDefinition(Opcode = 0x49, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x45, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x55, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x4D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x5D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x59, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x41, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x51, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void EOR() => A ^= AddressRead();

        /// <summary>
        /// Logical shift Right
        /// </summary>
        [OpcodeDefinition(Opcode = 0x0A, Mode = Direct)]
        [OpcodeDefinition(Opcode = 0x06, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0x16, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x0E, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x1E, Mode = AbsoluteX, Cycles = 7)]
        private void LSR()
        {
            // Get M value
            var newValue = AddressRead();

            // Check flags
            Flags.Carry = (newValue & 0x1) > 0;

            // Shift value
            newValue >>= 1;

            HandleFlags(newValue);

            // Write the new value
            AddressWrite(newValue);
        }

        /// <summary>
        /// ORs M and Accumulator
        /// </summary>
        [OpcodeDefinition(Opcode = 0x09, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x05, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x15, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x0D, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x1D, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x19, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0x01, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x11, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void ORA() => A |= AddressRead();

        /// <summary>
        /// Rotate one bit left (M or A)
        /// </summary>
        [OpcodeDefinition(Opcode = 0x0A, Mode = Direct)]
        [OpcodeDefinition(Opcode = 0x06, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0x16, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x0E, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x1E, Mode = AbsoluteX, Cycles = 7)]
        private void ROL()
        {
            // Get M value
            var newValue = AddressRead();

            // Handle flags
            var Carry = Flags.Carry;
            Flags.Carry = (newValue & 0x80) > 0;

            // Rotate
            newValue = newValue <<= 1;

            if (Carry) newValue |= 0x1;
            HandleFlags(newValue);

            // Write the new value
            AddressWrite(newValue);
        }

        /// <summary>
        /// Rotate one bit right (M or A)
        /// </summary>
        [OpcodeDefinition(Opcode = 0x0A, Mode = Direct)]
        [OpcodeDefinition(Opcode = 0x06, Mode = ZeroPage, Cycles = 5)]
        [OpcodeDefinition(Opcode = 0x16, Mode = ZeroPageX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x0E, Mode = Absolute, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0x1E, Mode = AbsoluteX, Cycles = 7)]
        private void ROR()
        {
            // Get M value
            var newValue = AddressRead();

            // Handle flags
            var Carry = Flags.Carry;
            Flags.Carry = (newValue & 0x1) > 0;

            // Rotate
            newValue = newValue >>= 1;

            if (Carry) newValue |= 0x80;
            HandleFlags(newValue);

            // Write the new value
            AddressWrite(newValue);
        }

        #endregion

        #region Branch Instructions

        /// <summary>
        /// Branch on carry clear
        /// </summary>
        [OpcodeDefinition(Opcode = 0x90, Cycles = 1)]
        private void BCC() => BranchInstruction(!Flags.Carry);

        /// <summary>
        /// Branch on carry set
        /// </summary>
        [OpcodeDefinition(Opcode = 0xB0, Cycles = 1)]
        private void BCS() => BranchInstruction(Flags.Carry);

        /// <summary>
        /// Branch on result zero
        /// </summary>
        [OpcodeDefinition(Opcode = 0xF0, Cycles = 1)]
        private void BEQ() => BranchInstruction(Flags.Zero);

        /// <summary>
        /// Branch on result minus
        /// </summary>
        [OpcodeDefinition(Opcode = 0x30, Cycles = 1)]
        private void BMI() => BranchInstruction(Flags.Negative);

        /// <summary>
        /// Branch on result not zero
        /// </summary>
        [OpcodeDefinition(Opcode = 0xD0, Cycles = 1)]
        private void BNE() => BranchInstruction(!Flags.Zero);

        /// <summary>
        /// Branch on result plus
        /// </summary>
        [OpcodeDefinition(Opcode = 0x10, Cycles = 1)]
        private void BPL() => BranchInstruction(Flags.Negative);

        /// <summary>
        /// Branch on overflow clear
        /// </summary>
        [OpcodeDefinition(Opcode = 0x50, Cycles = 1)]
        private void BVC() => BranchInstruction(!Flags.Overflow);

        /// <summary>
        /// Branch on overflow set
        /// </summary>
        [OpcodeDefinition(Opcode = 0x70, Cycles = 1)]
        private void BVS() => BranchInstruction(Flags.Overflow);

        /// <summary>
        /// Implements branch instruction
        /// </summary>
        /// <param name="flag">The flag control</param>
        private void BranchInstruction(bool flag)
        {
            if (flag)
            {
                PC = (uint)(PC + (sbyte)NextByte() + 1);
                Cycle++;
            }
        }

        #endregion

        #region Jump Instructions

        /// <summary>
        /// Jump to the address
        /// </summary>
        [OpcodeDefinition(Opcode = 0x4C, Mode = Absolute, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x6C, Mode = Indirect, Cycles = 4)]
        private void JMP()
        {
            if (CurrentOpcode == 0x4C)
                PC = NextWord();
            else if (CurrentOpcode == 0x6C)
            {
                var Value = NextWord();

                // AN INDIRECT JUMP MUST NEVER USE A VECTOR BEGINNING ON THE LAST BYTE OF A PAGE
                //
                // If address $3000 contains $40, $30FF contains $80, and $3100 contains $50, 
                // the result of JMP ($30FF) will be a transfer of control to $4080 rather than
                // $5080 as you intended i.e. the 6502 took the low byte of the address from
                // $30FF and the high byte from $3000.
                //
                // http://www.6502.org/tutorials/6502opcodes.html

                var high = (Value & 0xFF) == 0xFF ? Value - 0xFF : Value + 1;
                var oldPC = PC;
                PC = ReadByte(Value) | (ReadByte(high) << 8);

                if ((oldPC & 0xFF00) != (PC & 0xFF00)) Cycle += 2;
            }
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Jump to Subrotine
        /// </summary>
        [OpcodeDefinition(Opcode = 0x20, Mode = Absolute, Cycles = 6)]
        private void JSR()
        {
            // Saves the address for the next operation
            PushWord(PC + 1);

            // Set new PC
            PC = NextWord();
        }

        /// <summary>
        /// Return from interruption
        /// </summary>
        [OpcodeDefinition(Opcode = 0x40, Cycles = 6)]
        private void RTI()
        {
            // Retrieve flags
            NextByte();
            P = Pop();
            PC = PopWord();

            // TODO: Verify jump instructions
        }

        /// <summary>
        /// Return from Subrotine
        /// </summary>
        [OpcodeDefinition(Opcode = 0x60, Cycles = 6)]
        private void RTS() => PC = PopWord() + 1;

        #endregion

        #region Registers Instructions

        /// <summary>
        /// Clear Carry flag
        /// </summary>
        [OpcodeDefinition(Opcode = 0x18, Cycles = 1)]
        private void CLC() => Flags.Carry = false;

        /// <summary>
        /// Clear decimal mode
        /// </summary>
        [OpcodeDefinition(Opcode = 0xD8, Cycles = 1)]
        private void CLD() => Flags.Decimal = false;

        /// <summary>
        /// Clear Interrupt mode
        /// </summary>
        [OpcodeDefinition(Opcode = 0x58, Cycles = 1)]
        private void CLI() => Flags.InterruptDisable = false;

        /// <summary>
        /// Clear overflow flag
        /// </summary>
        [OpcodeDefinition(Opcode = 0xB8, Cycles = 1)]
        private void CLV() => Flags.Overflow = false;

        /// <summary>
        /// Compare M and A
        /// </summary>
        [OpcodeDefinition(Opcode = 0xC9, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xC5, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xD5, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xCD, Mode = Absolute, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xDD, Mode = AbsoluteX, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xD9, Mode = AbsoluteY, Cycles = 4, PageBoundary = true)]
        [OpcodeDefinition(Opcode = 0xC1, Mode = IndirectX, Cycles = 6)]
        [OpcodeDefinition(Opcode = 0xD1, Mode = IndirectY, Cycles = 5, PageBoundary = true)]
        private void CMP() => CMPInstruction(A);

        /// <summary>
        /// Compares M and X
        /// </summary>
        [OpcodeDefinition(Opcode = 0xE0, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xE4, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xEC, Mode = Absolute, Cycles = 4)]
        private void CPX() => CMPInstruction(X);

        /// <summary>
        /// Compares M and Y
        /// </summary>
        [OpcodeDefinition(Opcode = 0xC0, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xC4, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0xCC, Mode = Absolute, Cycles = 4)]
        private void CPY() => CMPInstruction(Y);

        /// <summary>
        /// Set carry flag
        /// </summary>
        [OpcodeDefinition(Opcode = 0x38, Cycles = 1)]
        private void SEC() => Flags.Carry = true;

        /// <summary>
        /// Set decimal flag
        /// </summary>
        [OpcodeDefinition(Opcode = 0xF8, Cycles = 1)]
        private void SED() => Flags.Decimal = true;

        /// <summary>
        /// Set interrupt flag
        /// </summary>
        [OpcodeDefinition(Opcode = 0x78, Cycles = 1)]
        private void SEI() => Flags.InterruptDisable = false;

        /// <summary>
        /// Implements the compare instruction
        /// </summary>
        /// <param name="register">The register to compare</param>
        private void CMPInstruction(uint register)
        {
            var value = register - (int)AddressRead();

            Flags.Negative = (value & 0x80) > 0 && value != 0;
            Flags.Carry = value >= 0;
            Flags.Zero = value == 0;
        }

        #endregion

        #region Stack Instructions

        /// <summary>
        /// Push A on Stack
        /// </summary>
        [OpcodeDefinition(Opcode = 0x48, Cycles = 1)]
        private void PHA() => Push(A);

        /// <summary>
        /// Push P on Stack
        /// </summary>
        [OpcodeDefinition(Opcode = 0x08, Cycles = 1)]
        private void PHP() => Push(P);

        /// <summary>
        /// Pull A from stack
        /// </summary>
        [OpcodeDefinition(Opcode = 0x68, Cycles = 1)]
        private void PLA() => A = Pop();

        /// <summary>
        /// Pull P from stack
        /// </summary>
        [OpcodeDefinition(Opcode = 0x28, Cycles = 1)]
        private void PLP() => P = Pop();

        #endregion

        #region System Instructions

        /// <summary>
        /// Force break
        /// </summary>
        [OpcodeDefinition(Opcode = 0x00, Cycles = 7)]
        private void BRK()
        {
            Push(P | 0x10);
            Flags.InterruptDisable = true;
            PC = ReadWord(InterruptsOffset[(int)InterruptTypes.IRQ]);
        }

        /// <summary>
        /// NOP (nothing) instruction
        /// </summary>
        [OpcodeDefinition(Opcode = 0xEA)]
        private void NOP() { }

        #endregion

        #region Unofficial Instructions

        /// <summary>
        /// AND Byte with the accumulator
        /// </summary>
        [OpcodeDefinition(Opcode = 0x0B, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x2B, Mode = Immediate)]
        private void AAC()
        {
            var newValue = A & AddressRead();

            // Update Flags
            Flags.Carry = (newValue & 0x80) > 0;
            HandleFlags(newValue);

            A = newValue;
        }

        /// <summary>
        /// AND M with the accumulator than transfer A to X
        /// </summary>
        [OpcodeDefinition(Opcode = 0xAB, Mode = Immediate)]
        private void ATX()
        {
            A &= AddressRead();
            X = A;
        }

        [OpcodeDefinition(Opcode = 0x04, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x14, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x34, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x44, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x54, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x64, Mode = ZeroPage, Cycles = 3)]
        [OpcodeDefinition(Opcode = 0x74, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0x80, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x82, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0x89, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xC2, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xD4, Mode = ZeroPageX, Cycles = 4)]
        [OpcodeDefinition(Opcode = 0xE2, Mode = Immediate)]
        [OpcodeDefinition(Opcode = 0xF4, Mode = ZeroPageX, Cycles = 4)]
        private void DOP() { }

        #endregion
    }
}
