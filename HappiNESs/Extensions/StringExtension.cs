using System.Runtime.CompilerServices;

namespace HappiNESs
{
    /// <summary>
    /// <see cref="string"/> extensions for the C# compiler
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        /// Converts <see cref="string"/> to <see cref="uint"/> type.
        /// </summary>
        /// <param name="to">The value to be converted</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToUint(this string to) => uint.Parse(to, System.Globalization.NumberStyles.HexNumber);
    }
}
