namespace HappiNESs
{
    /// <summary>
    /// The types of Dialog Windows
    /// </summary>
    public enum DialogWindowType
    {
        /// <summary>
        /// A Dialog window to create a new user
        /// </summary>
        NewUserDialog = 0,

        /// <summary>
        /// A Dialog window to configure a Controlled parameter on Timeline
        /// </summary>
        ControlledTimelineDialog = 1,

        /// <summary>
        /// A custom message box depending on parameters
        /// </summary>
        MessageBoxDialog = 2,

        /// <summary>
        /// A property window for an experiment file
        /// </summary>
        ExperimentPropertyWindow = 3,

        /// <summary>
        /// The pH Calibration wizard for Two Points
        /// </summary>
        PHTwoPointsCalibrationWizard = 4,

        /// <summary>
        /// The About application dialog
        /// </summary>
        AboutDialog = 5,

        /// <summary>
        /// The Update notes dialog
        /// </summary>
        UpdateNotesDialog = 6,
    }
}
