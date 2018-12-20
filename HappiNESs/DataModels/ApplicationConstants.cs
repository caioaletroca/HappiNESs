namespace HappiNESs
{
    /// <summary>
    /// Constants used in the application
    /// </summary>
    public static class ApplicationConstants
    {
        #region Application Misc

        /// <summary>
        /// The number of measure values to force autosave experiment
        /// </summary>
        public const int AutoSaveDataCount = 300;

        /// <summary>
        /// Minimum acceptible quality level for the pH sensor
        /// </summary>
        public const int PHSensorMinimumQualityLevel = 80;

        /// <summary>
        /// The Update notes file path
        /// </summary>
        public const string UpdateNotesPath = "updatenotes.json";

        #endregion

        #region File Constants

        /// <summary>
        /// iNES file extension
        /// </summary>
        public const string NESFileExtension = ".nes";

        /// <summary>
        /// Json file extension
        /// </summary>
        public const string JsonFileExtension = ".json";

        /// <summary>
        /// Text file extension
        /// </summary>
        public const string TextFileExtension = ".txt";

        /// <summary>
        /// The default log file name
        /// </summary>
        public const string DefaultLogFileName = "logfile";

        /// <summary>
        /// The <see cref="NESFileExtension"/> file filter for System dialogs
        /// </summary>
        public const string NESFileFilter = "iNES (*" + NESFileExtension + ")|*" + NESFileExtension;

        /// <summary>
        /// All files filter for System dialogs
        /// </summary>
        public const string AllFileFilter = "Todos Arquivos (*.*)|*.*";

        /// <summary>
        /// The CSV file filter for System Dialogs
        /// </summary>
        public const string CSVFileFilter = "CSV (*.csv)|*.csv";

        /// <summary>
        /// The Json file filter for System Dialogs
        /// </summary>
        public const string JSONFileFilter = "Json (*.json)|*.json";

        /// <summary>
        /// The TXT file filter for System Dialogs
        /// </summary>
        public const string TxtFileFilter = "Texto (*.txt)|*.txt";

        #endregion
    }
}
