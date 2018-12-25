using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappiNESs.Emulator
{
    /// <summary>
    /// The Picture Processing Unity component
    /// </summary>
    internal class PPU
    {
        #region Public Properties

        public bool VBlank { get; set; }

        #endregion

        #region Private Properties

        private int Cycles { get; set; }

        #endregion

        private byte[] PrimaryOAM { get; set; } = new byte[64];

        private byte[] SecondaryOAM { get; set; } = new byte[8];

        #region Rendering Methods

        public void SetPixel()
        {

        }

        #endregion
    }
}
