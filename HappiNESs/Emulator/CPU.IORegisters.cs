using System.Runtime.CompilerServices;

namespace HappiNESs
{
    internal sealed partial class CPU
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteIORegister(uint Register, byte Value)
        {
            switch(Register)
            {
                default:
                    break;
            }

            if (Register <= 0x401F) return;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ReadIORegister(uint Register)
        {
            // TODO: Implement IO Readings and writings
            return 0x00;
        }
    }
}
