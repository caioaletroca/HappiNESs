namespace HappiNESs
{
    /// <summary>
    /// Interface for Serializers
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize an object to a string
        /// </summary>
        /// <param name="Data">The data to be converted</param>
        /// <returns></returns>
        string Convert(object Data);

        /// <summary>
        /// Restore a string to it Object
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="Json">The json string to be restored</param>
        /// <returns></returns>
        object Restore<T>(string String);
    }
}
