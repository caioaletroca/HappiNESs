namespace HappiNESs
{
    /// <summary>
    /// Returns all the important imformation from a System Dialog Window
    /// </summary>
    public class SystemDialogResult
    {
        /// <summary>
        /// The name for the file
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// The path for the file
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// The Selected Path
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        /// The Dialog result as <see cref="DialogResultType"/>
        /// </summary>
        public int Result { get; set; }
    }
}
