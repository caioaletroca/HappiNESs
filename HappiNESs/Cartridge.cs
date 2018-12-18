using System;
using System.IO;

namespace HappiNESs
{
    /// <summary>
    /// Handles the ROM loading and manipulation
    /// </summary>
    public class Cartridge
    {
        #region Private Properties

        /// <summary>
        /// The raw ROM loaded as an array of <see cref="byte"/>
        /// </summary>
        public readonly byte[] Rom;

        public readonly int PRGROMSize;

        public readonly int CHRROMSize;

        public readonly int PRGRAMSize;

        public readonly byte Flag6;

        public readonly byte PRGROMOffset;

        public readonly byte[] PRGROM;

        public readonly byte[] CHRROM;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Cartridge(string path)
        {
            // Read all the rom file
            Rom = File.ReadAllBytes(path);

            // Check if the rom has the iNES header
            var header = BitConverter.ToInt32(Rom, 0);
            if (header != 0x1A53454E)
                throw new FormatException($"Unexpected file header for {path}");

            // Get sizes
            PRGROMSize = Rom[4] * 0x4000; // 16kb units
            CHRROMSize = Rom[5] * 0x2000; // 8kb units
            PRGRAMSize = Rom[8] * 0x2000;

            // Get flags
            Flag6 = Rom[6];

            PRGROMOffset = 16;

            PRGROM = new byte[PRGROMSize];
            Array.Copy(Rom, PRGROMOffset, PRGROM, 0, PRGROMSize);

            if (CHRROMSize == 0)
                CHRROM = new byte[0x200];
            else
            {
                CHRROM = new byte[CHRROMSize];
            }
        }

        #endregion
    }
}
