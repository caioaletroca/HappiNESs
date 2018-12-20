namespace HappiNESs
{
    /// <summary>
    /// Handles the Application Settings from Visual Studio
    /// </summary>
    public interface IStoreManager
    {
        /// <summary>
        /// Reads a settings from storage
        /// </summary>
        /// <param name="key">Key for data access</param>
        /// <returns></returns>
        object Read(string key);

        /// <summary>
        /// Writes a setting value to storage
        /// </summary>
        /// <param name="key">Key for data access</param>
        /// <param name="data">Value to be stored</param>
        void Write(string key, object data);

        /// <summary>
        /// Saves the store
        /// </summary>
        void Save();

        /// <summary>
        /// Reset to the default value a property
        /// </summary>
        /// <param name="key">The property to be resetted</param>
        void Reset(string key);
    }
}
