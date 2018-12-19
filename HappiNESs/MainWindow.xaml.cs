using System;
using System.Collections.Generic;
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
            nes.LoadRom("nestest.nes");
            nes.CPU.Initialize();

            //for(var i = 0; ; i++)
            //{
            //    var test = nes.CPU.ReadByte((uint)i);
            //    Console.WriteLine($"Address: {i}, {test}");
            //    if (test == 0xFF)
            //        Debugger.Break();
            //}
            nes.CPU.PC = 0xC000;

            // Load file

            for (var i = 0; i < 100000; i++)
            {

                nes.CPU.ExecuteInstruction();
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
