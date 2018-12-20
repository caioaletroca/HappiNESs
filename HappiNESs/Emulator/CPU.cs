using System;
using System.Linq;
using System.Reflection;

namespace HappiNESs
{
    /// <summary>
    /// The Main core for the CPU 6502
    /// </summary>
    internal sealed partial class CPU : Addressable
    {
        #region Public Properties

        /// <summary>
        /// The current CPU cycle
        /// </summary>
        public int Cycle;

        /// <summary>
        /// The current loaded opcode
        /// </summary>
        public uint CurrentOpcode { get; set; }

        /// <summary>
        /// The memory RAM
        /// </summary>
        public byte[] Ram { get; set; } = new byte[0x800];

        #endregion

        #region Delegates

        /// <summary>
        /// Opcode delegate
        /// </summary>
        public delegate void Opcode();

        #endregion

        #region Private Properties

        /// <summary>
        /// The array containing all the CPU opcodes
        /// </summary>
        private readonly Opcode[] Opcodes = new Opcode[256];

        /// <summary>
        /// The array containing all the CPU opcodes definitions
        /// </summary>
        private readonly OpcodeDefinition[] OpcodeDefinitions = new OpcodeDefinition[256];

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CPU(NESConsole console) : base(0xFFFF)
        {
            InitializeMemoryMap();
            InitializeOpcodes();
            Initialize();
        }

        /// <summary>
        /// Initialize the Opcodes arrays
        /// </summary>
        private void InitializeOpcodes()
        {
            // Construct the bindings
            var opcodeBindings = from opcode in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                 let defs = opcode.GetCustomAttributes(typeof(OpcodeDefinition), false)
                                 where defs.Length > 0
                                 select new
                                 {
                                     binding = (Opcode)Delegate.CreateDelegate(typeof(Opcode), this, opcode.Name),
                                     name = opcode.Name,
                                     defs = (from d in defs select (OpcodeDefinition)d),
                                 };

            foreach (var opcode in opcodeBindings)
                foreach (var def in opcode.defs)
                {
                    Opcodes[def.Opcode] = opcode.binding;
                    OpcodeDefinitions[def.Opcode] = def;
                }
        }

        #endregion
    }
}
