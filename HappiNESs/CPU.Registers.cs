using System.Runtime.CompilerServices;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        #region Registers

        public uint mA;

        public uint mX;

        public uint mY;

        public uint mSP;

        public uint PC;

        #endregion

        #region Class Definitions

        /// <summary>
        /// The CPU status flags stored in the P register
        /// </summary>
        public class CPUFlags
        {
            public bool Carry { get; set; }

            public bool Zero { get; set; }

            public bool InterruptDisable { get; set; }

            public bool Decimal { get; set; }

            public bool BreakSource { get; set; }

            public bool Overflow { get; set; }

            public bool Negative { get; set; }
        }

        /// <summary>
        /// Instance for the CPU flags
        /// </summary>
        public readonly CPUFlags Flags = new CPUFlags();

        #endregion

        #region Getters and Setters

        /// <summary>
        /// The Accumulator register
        /// </summary>
        public uint A
        {
            get => mA;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => mA = HandleFlags(value & 0xFF);
        }

        /// <summary>
        /// The X register
        /// </summary>
        public uint X
        {
            get => mX;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => mX = HandleFlags(value & 0xFF);
        }

        /// <summary>
        /// The Y register
        /// </summary>
        public uint Y
        {
            get => mY;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => mY = HandleFlags(value & 0xFF);
        }

        /// <summary>
        /// The Stack Pointer register
        /// </summary>
        public uint SP
        {
            get => mSP;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => mSP = (value & 0xFF);
        }

        /// <summary>
        /// Handles the Negative and the Zero flags setting
        /// </summary>
        /// <param name="Value">The value to be analyzed</param>
        public uint HandleFlags(uint Value)
        {
            Flags.Zero = (Value & 0xFF) == 0;
            Flags.Negative = (Value & 0x80) > 0;
            return Value;
        }

        /// <summary>
        /// The Process Status registers. Hold information about several flags in the system
        /// </summary>
        public uint P
        {
            get => (uint)(
                    (Flags.Carry.ToByte() << 0) |
                    (Flags.Zero.ToByte() << 1) |
                    (Flags.InterruptDisable.ToByte() << 2) |
                    (Flags.Decimal.ToByte() << 3) |
                    (Flags.BreakSource.ToByte() << 4) |
                    (1 << 5) |
                    (Flags.Overflow.ToByte() << 6) |
                    (Flags.Negative.ToByte() << 7)
                );
            set
            {
                Flags.Carry = (value & 0x1) > 0;
                Flags.Zero = (value & 0x2) > 0;
                Flags.InterruptDisable = (value & 0x4) > 0;
                Flags.Decimal = (value & 0x8) > 0;
                Flags.BreakSource = (value & 0x10) > 0;
                Flags.Overflow = (value & 0x40) > 0;
                Flags.Negative = (value & 0x80) > 0;
            }
        }

        #endregion
    }
}
