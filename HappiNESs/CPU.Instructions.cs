using System;
using System.Reflection;

namespace HappiNESs
{
    sealed partial class CPU
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
        public class OpcodeDefinition : Attribute
        {
            public int Opcode;
        }

        [OpcodeDefinition(Opcode = 0x0100)]
        [OpcodeDefinition(Opcode = 0x0010)]
        private void Pop()
        {
            
        }

        public CPU()
        {
            //var binding = from opcode in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            //var binding = (Opcode)Delegate.CreateDelegate(typeof(Opcode), this, (0x200).ToString());

            //iterating through the method attribtues
            foreach (MethodInfo m in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                
                foreach (Attribute a in m.GetCustomAttributes(typeof(OpcodeDefinition), false))
                {
                    OpcodeDefinition dbi = (OpcodeDefinition)a;

                    if (null != dbi)
                    {
                        Console.WriteLine(dbi.Opcode);
                    }
                }
            }

        }
    }
}
