namespace HappiNESs
{
    /// <summary>
    /// A model for a <see cref="CPU"/> state
    /// </summary>
    public class CPUState
    {
        #region Public Properties

        /// <summary>
        /// The cpu Program counter
        /// </summary>
        public uint PC { get; set; }

        /// <summary>
        /// The current opcode
        /// </summary>
        public uint Opcode { get; set; }

        /// <summary>
        /// The accumulator value
        /// </summary>
        public uint A { get; set; }

        /// <summary>
        /// The X register value
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// The Y register value
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// The Program Status value
        /// </summary>
        public uint P { get; set; }

        /// <summary>
        /// The stack pointer value
        /// </summary>
        public uint SP { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares two <see cref="CPUState"/> objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CPUState))
                return false;

            var other = obj as CPUState;

            if (
                PC != other.PC &&
                A != other.A &&
                X != other.X &&
                Y != other.Y &&
                P != other.P &&
                SP != other.SP
            )
                return false;

            return true;
        }

        /// <summary>
        /// The equality operator override
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(CPUState x, CPUState y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// The inequality operator override
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(CPUState x, CPUState y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Converts to a <see cref="string"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{PC.ToString("X4")} \t A:{A.ToString("X2")} X:{X.ToString("X2")} Y:{Y.ToString("X2")} P:{P.ToString("X2")} SP:{SP.ToString("X2")}";
        }

        #endregion
    }
}
