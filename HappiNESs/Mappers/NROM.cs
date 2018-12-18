namespace HappiNESs
{
    internal class NROM : BaseMappers
    {
        #region Private Properties

        private readonly byte[] AddressSpace = new byte[0x2000 + 0x8000];

        #endregion

        #region Constructor

        public NROM(NESConsole console) : base(console)
        {
            for (var i = 0; i < 0x8000; i++)
            {
                var Offset = console.Cartridge.PRGROMSize == 0x4000 ? i & 0xBFFF : i;
                AddressSpace[0x2000 + i] = PRGROM[Offset];
            }

            InitializeMemoryMap(console.CPU);
        }

        #endregion

        #region Mappers Methods

        public override void InitializeMemoryMap(CPU cpu)
        {
            cpu.MapReadHandler(0x6000, 0xFFFF, Address => AddressSpace[Address - 0x6000]);
            cpu.MapWriteHandler(0x6000, 0xFFFF, (Address, Value) => AddressSpace[Address - 0x6000] = Value);
        }

        #endregion
    }
}
