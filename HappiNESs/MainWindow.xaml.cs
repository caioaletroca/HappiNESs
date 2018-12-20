using System;
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

            // TODO: Remove debug code
            //var nes = new NESConsole();
            //nes.LoadRom("nestest.nes");
            //nes.CPU.Initialize();

            //nes.CPU.PC = 0xC000;

            //// Load file

            //for (var i = 0; i < 100000; i++)
            //{

            //    nes.CPU.ExecuteInstruction();
            //}


            //for(var i = 0; ;i++)
            //{
            //    var Read = nes.CPU.ReadByte(0x6004 + (uint)i);

            //    if (Read == 0)
            //        break;
            //    else
            //        Console.Write((char)Read);
            //}
        }
    }
}
