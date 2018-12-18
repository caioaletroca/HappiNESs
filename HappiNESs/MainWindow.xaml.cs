using System;
using System.Diagnostics;
using System.Windows;

namespace HappiNESs
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var nes = new NESConsole();
            nes.LoadRom("01-abs_x_wrap.nes");
            nes.CPU.Initialize();

            //for(var i = 0; ; i++)
            //{
            //    var test = nes.CPU.ReadByte((uint)i);
            //    Console.WriteLine($"Address: {i}, {test}");
            //    if (test == 0xFF)
            //        Debugger.Break();
            //}

            for (var i = 0; i < 100000; i++)
            {
                nes.CPU.ExecuteInstruction();

                Console.WriteLine($"PC: {nes.CPU.PC.ToString("X")}");
                Console.WriteLine($"Opcode: {nes.CPU.CurrentOpcode.ToString("X")}");

                var test = nes.CPU.ReadByte(0x6000);

                Console.WriteLine($"Test Flag: {test.ToString("X")}");
                Console.WriteLine($"SP: {nes.CPU.ReadByte(nes.CPU.SP + 1).ToString("X")}");

                if (nes.CPU.PC == 0xE7B5)
                    break;
            }


            for(var i = 0; ;i++)
            {
                var Read = nes.CPU.ReadByte(0x6004 + (uint)i);

                if (Read == 0)
                    break;
                else
                    Console.Write((char)Read);
            }
        }
    }
}
