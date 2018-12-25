using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace HappiNESs
{
    /// <summary>
    /// Handles rendering with OpenGL
    /// </summary>
    public class OpenGLRenderer : UserControl, IRenderer
    {
        #region Public Properties

        /// <summary>
        /// The renderer name
        /// </summary>
        public string RendererName => "OpenGL";

        /// <summary>
        /// The <see cref="OpenGL"/> instance
        /// </summary>
        public OpenGL GL { get; set; }

        #endregion

        #region Private Properties

        private Bitmap Image { get; set; }

        private Texture Tex { get; set; }

        #endregion

        #region Render Methods

        /// <summary>
        /// Draws on the screen
        /// </summary>
        public void Draw()
        {
            // Run the task async in the pool
            var task = AsyncAwaiter.AwaitAsync(RendererName, async () =>
            {
                // Clears screen
                GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                // TODO: Remove in final version
                GL.ClearColor(Color.Gray.R / 255.0f, Color.Gray.G / 255.0f, Color.Gray.B / 255.0f, 0);
                
                // Set ortographic projection
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Viewport(0, 0, (int)ActualWidth, (int)ActualHeight);
                GL.Ortho(0d, 1d, 0d, 1d, -1d, 1d);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                // Re-binds the texture
                Tex.Bind(GL);

                // Draw image render rectangle that covers whole screen
                GL.Begin(BeginMode.Quads);
                GL.TexCoord(0, 1);
                GL.Vertex(0, 0);
                GL.TexCoord(0, 0);
                GL.Vertex(0, 1);
                GL.TexCoord(1, 0);
                GL.Vertex(1, 1);
                GL.TexCoord(1, 1);
                GL.Vertex(1, 0);
                GL.End();
            });
        }

        /// <summary>
        /// Initialize the rendering
        /// </summary>
        public void Initialize()
        {
            // Loads bitmap
            Image = new Bitmap(@"C:\Users\caio_\OneDrive\DSC_0496.jpg");

            Tex = new Texture();
            Tex.Create(GL, Image);

            // Enable textures
            GL.Enable(OpenGL.GL_TEXTURE_2D);
        }

        #endregion
    }
}
