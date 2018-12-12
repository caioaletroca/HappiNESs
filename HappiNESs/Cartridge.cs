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
        private readonly byte[] Rom;

        private readonly int PRGROM;

        private readonly int CHRROM;

        private readonly int PRGRAM;

        private readonly byte Flag6;

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
            int header = BitConverter.ToInt32(Rom, 0);
            if (header != 0x1A53454E)
                throw new FormatException($"Unexpected file header for {path}");

            // Get sizes
            PRGROM = Rom[4] * 0x4000; // 16kb units
            CHRROM = Rom[5] * 0x2000; // 8kb units
            PRGRAM = Rom[8] * 0x2000;

            // Get flags
            Flag6 = Rom[6];
        }

        #endregion
    }
}
