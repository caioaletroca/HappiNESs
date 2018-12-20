using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappiNESs
{
    /// <summary>
    /// Interação lógica para EmulatorScreen.xam
    /// </summary>
    public partial class EmulatorScreen : UserControl
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmulatorScreen()
        {
            InitializeComponent();
        }

        #endregion

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

        }
    }
}
