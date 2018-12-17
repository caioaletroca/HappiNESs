using System.Runtime.CompilerServices;

namespace HappiNESs
{
    /// <summary>
    /// Handles the memory manipulation methods and systems
    /// </summary>
    internal abstract class Addressable
    {
        #region Public Properties

        public readonly uint AddressSize;

        public readonly ReadDelegate[] ReadMap;

        public readonly WriteDelegate[] WriteMap;

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate to read a byte in a specific memory location
        /// </summary>
        /// <param name="address">The address to read</param>
        /// <returns></returns>
        public delegate uint ReadDelegate(uint address);

        /// <summary>
        /// Delegate to write a byte in a specific memory location
        /// </summary>
        /// <param name="address">The address to write</param>
        /// <param name="value">The value to write</param>
        public delegate void WriteDelegate(uint address, byte value);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="AddressSize"></param>
        protected Addressable(uint AddressSize = 0x1000)
        {
            this.AddressSize = AddressSize;
            ReadMap = new ReadDelegate[AddressSize + 1];
            WriteMap = new WriteDelegate[AddressSize + 1];
        }

        /// <summary>
        /// Resets the memory map state
        /// </summary>
        protected virtual void InitializeMemoryMap()
        {
            // Resets mappers
            ReadMap.Fill(Address => 0);
            WriteMap.Fill((Address, Value) => { });
        }

        #endregion

        #region R/W Methods

        /// <summary>
        /// Reads a single byte using the memory mappings system
        /// </summary>
        /// <param name="Address">The address to read the byte</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ReadByte(uint Address)
        {
            // Snaps to maximum address size
            Address &= AddressSize;

            // Read from memory
            return ReadMap[Address](Address);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteByte(uint Address, uint Value)
        {
            // Snaps to maximum address size
            Address &= AddressSize;

            // Writes to the memory
            WriteMap[Address](Address, (byte)Value);
        }

        #endregion

        #region Mapping Handlers

        /// <summary>
        /// Adds a map reader to the memory
        /// </summary>
        /// <param name="start">The start address</param>
        /// <param name="end">The end address</param>
        /// <param name="action">The method to implement</param>
        public void MapReadHandler(uint start, uint end, ReadDelegate action)
        {
            for (var i = start; i <= end; i++)
                ReadMap[i] = action;
        }

        public void MapWriteHandler(uint start, uint end, WriteDelegate action)
        {
            for (var i = start; i <= end; i++)
                WriteMap[i] = action;
        }

        #endregion
    }
}
