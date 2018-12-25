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
    internal class PPU : Addressable
    {
        #region Enum Definitions

        /// <summary>
        /// Defines the rendering state for a <see cref="PPU"/>
        /// </summary>
        public enum PPURenderState
        {
            PreRender, Render, PosRender, VBlank
        }

        public enum PPUCycleState
        {
            PreFetch, VisibleLines, PosFetch
        }

        #endregion

        #region Public Properties

        public bool VBlank { get; set; }

        private const int GameWidth = 256, GameHeight = 240;
        public readonly uint[] RawBitmap = new uint[GameWidth * GameHeight];

        public PPURenderState RenderState { get; set; }

        public PPUCycleState CycleState { get; set; }

        #endregion

        #region Private Properties

        private int Cycles { get; set; }

        private const int ScanlinesCount = 261;

        private const int CyclesPerScanline = 340;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PPU() : base(0x3FFF)
        {
            InitializeMemoryMap();
        }

        #endregion

        private byte[] PrimaryOAM { get; set; } = new byte[64];

        private byte[] SecondaryOAM { get; set; } = new byte[8];

        #region Rendering Methods

        /// <summary>
        /// Process the entire screen frame
        /// </summary>
        public void ProcessFrame()
        {
            RawBitmap.Fill(0u);

            for (var i = -1; i < ScanlinesCount; i++)
                ProcessScanline(i);
        }

        /// <summary>
        /// Process a unique scanline
        /// </summary>
        /// <param name="Scanline">The current scanline</param>
        public void ProcessScanline(int Scanline)
        {
            if (Scanline == -1 && Scanline == 261)
                RenderState = PPURenderState.PreRender;
            else if (Scanline > -1 && Scanline <= 239)
                RenderState = PPURenderState.Render;
            else if (Scanline == 240)
                RenderState = PPURenderState.PosRender;
            else if (Scanline >= 241 && Scanline <= 261)
                RenderState = PPURenderState.VBlank;

            for (var i = 0; i < CyclesPerScanline; i++)
                ProcessCycle(Scanline, i);
        }

        /// <summary>
        /// Process a unique cycle
        /// </summary>
        /// <param name="Scanline">The current scanline</param>
        /// <param name="Cycle">The current cycle</param>
        public void ProcessCycle(int Scanline, int Cycle)
        {
            if (Cycle >= 1 && Cycle <= 256)
                CycleState = PPUCycleState.VisibleLines;
                
        }

        #endregion
    }
}
