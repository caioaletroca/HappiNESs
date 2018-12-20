namespace HappiNESs
{
    /// <summary>
    /// Handles the serial port communication with a external device
    /// </summary>
    public interface ISerial
    {
        /// <summary>
        /// Connects with the serial port
        /// </summary>
        void Connect();

        /// <summary>
        /// Reads data from the serial port
        /// </summary>
        /// <returns></returns>
        dynamic Read();

        /// <summary>
        /// Writes a value on the serial port
        /// </summary>
        /// <param name="Value"></param>
        void Write(object Value);
    }
}
