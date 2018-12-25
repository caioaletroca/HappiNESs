using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using System.Drawing;
using System.Drawing.Imaging;

namespace HappiNESs
{
    /// <summary>
    /// Interação lógica para EmulatorScreen.xam
    /// </summary>
    public partial class EmulatorScreen : OpenGLRenderer
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

        public const int GameWidth = 256;
        public const int GameHeight = 240;
        public uint[] rawBitmap = new uint[GameWidth * GameHeight];

        #region WPF Events

        /// <summary>
        /// Fires when OpenGL is initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            // Initialize instance
            GL = args.OpenGL;

            // Initialize Renderer
            Initialize();
        }

        /// <summary>
        /// Fires on every OpenGL draw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            Draw();
        }

        #endregion
    }
}
