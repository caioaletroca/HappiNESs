using System.Runtime.CompilerServices;

namespace HappiNESs
{
    /// <summary>
    /// <see cref="bool"/> extensions for the C# compiler
    /// </summary>
    internal static class BooleanExtension
    {
        /// <summary>
        /// Converts <see cref="bool"/> to <see cref="byte"/> type.
        /// </summary>
        /// <param name="to">The value to be converted</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe byte ToByte(this bool to) => *(byte*)&to;

        /// <summary>
        /// Converts <see cref="bool"/> to <see cref="int"/> type
        /// </summary>
        /// <param name="to">The value to be converted</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this bool to) => to ? 1 : 0;
    }
}
