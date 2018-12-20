using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BioFocoApp.Core
{
    /// <summary>
    /// Handles the main process system for manipulating all the reactor devices
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TaskProcess : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A flag that represents if the task process is active
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// A flag that represents if we are writing setpoints to the devices
        /// Also, represents if the experiment is running on the timeline
        /// </summary>
        public bool Writings { get; set; }

        /// <summary>
        /// A <see cref="List{Boolean}"/> of flags that represents if we are reading data from each of devices
        /// Currently, the order is: <see cref="DataLoggerModBus"/>, <see cref="MotorModBus"/>, <see cref="BalanceSerial"/> and <see cref="PHSensorModBus"/>
        /// </summary>
        public List<bool> Readings { get; set; } = new List<bool>() { false, false, false, false, false };

        /// <summary>
        /// The interval for every run on the main process
        /// </summary>
        public int Tick { get; set; }

        /// <summary>
        /// Stores the experiment start time
        /// </summary>
        [JsonProperty]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Stores the experiment end time
        /// </summary>
        [JsonProperty]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The elapsed time during the experiment running
        /// </summary>
        [JsonProperty]
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.FromSeconds(0);

        #endregion

        #region Delegates

        /// <summary>
        /// The default <see cref="TaskProcess"/> event handler
        /// </summary>
        /// <param name="sender">Reference to <see cref="TaskProcess"/></param>
        public delegate void TaskProcessEventHandler(object sender);

        #endregion

        #region Events

        /// <summary>
        /// Event fired every <see cref="Tick"/> time by the main process
        /// </summary>
        public event TaskProcessEventHandler OnRun;

        /// <summary>
        /// Event fired when starting the process
        /// </summary>
        public event TaskProcessEventHandler OnStartRun;

        /// <summary>
        /// Event fired when stopping the process
        /// </summary>
        public event TaskProcessEventHandler OnStopRun;

        /// <summary>
        /// Event fired very before the main process first run
        /// </summary>
        public event TaskProcessEventHandler OnBeforeRun;

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the current process
        /// </summary>
        public void Start()
        {
            // Avoid start more than one process
            if (IsRunning)
                return;

            // Set control flag
            IsRunning = true;

            // Fires onstart event
            OnStartRun?.Invoke(this);

            // Save the start time if the marker is on the start position
            if (ElapsedTime.TotalSeconds == 0)
                StartTime = DateTime.Now;

            // Run process
            Run();
        }

        /// <summary>
        /// Full start all the writings and readings from the process
        /// </summary>
        public void StartAll()
        {
            // Set all the flags
            Writings = true;
            Readings = Readings.Select(x => true).ToList();

            // Avoid start more than one process
            if (IsRunning)
                return;

            // Normal Start
            Start();
        }

        /// <summary>
        /// Stops the current process
        /// </summary>
        public void Stop()
        {   
            // Set control flag
            IsRunning = false;

            // Clear control flags
            Readings = Readings.Select(x => false).ToList();
            Writings = false;

            // Save the end time
            EndTime = DateTime.Now;

            // Fires onstop event
            OnStopRun?.Invoke(this);
        }

        /// <summary>
        /// Clear the process to start fresh again
        /// </summary>
        public void Clear()
        {
            // Clear Elapsed
            ElapsedTime = TimeSpan.Zero;
        }

        /// <summary>
        /// The main process thread.
        /// Rises <see cref="OnRun"/> event every <see cref="Tick"/> time elapsed
        /// </summary>
        public void Run()
        {
            // Rise onbefore event
            OnBeforeRun?.Invoke(this);

            // Get the tick period in seconds
            Tick = IoC.Application.TaskRunnerTick;

            // Starts the new thread
            IoC.Task.Run(() =>
            {
                // Initialize the control variable
                var LastRun = DateTime.Now;

                // Run as long as the control flag is true
                while (IsRunning)
                {
                    // Ticks every previus defined second
                    if (DateTime.Now - LastRun >= TimeSpan.FromSeconds(Tick))
                    {
                        // Saves the time this run happens
                        LastRun = DateTime.Now;

                        // The time since last iteration
                        var IntervalTime = DateTime.Now - LastRun;

                        // Diagnostics tools to reveal the timestamp in code
                        var Watch = new Stopwatch();
                        Watch.Start();

                        // Increment time elapsed only with fullplay
                        if(Writings)
                            ElapsedTime += IntervalTime;

                        // Run the principal event
                        OnRun?.Invoke(this);

                        // Evaluate finish conditions only if we do not wanted to force stop
                        // Kill this thread if nobody is using
                        if (IsRunning)
                            IsRunning = IsReadings() || Writings;

                        // Stop watch time
                        Watch.Stop();
                        
                        // Log
                        IoC.Logger.Log($"Elapsed Time {ElapsedTime.ToString()}, and time in operations {Watch.ElapsedMilliseconds.ToString()}", LogLevel.Debug);
                    }
                }
            });
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Check if there is any true value in the <see cref="Readings"/> list
        /// </summary>
        /// <returns></returns>
        public bool IsReadings()
        {
            return Readings.Any(x => x);
        }

        #endregion
    }
}
