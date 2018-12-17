namespace HappiNESs
{
    /// <summary>
    /// Extends methods for the Array types
    /// </summary>
    internal static class ArrayExtension
    {
        /// <summary>
        /// Fills an array with the specific value
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="array">The array to be filled</param>
        /// <param name="value">The value to fill</param>
        public static void Fill<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
    }
}
