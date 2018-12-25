using SharpGL;
using System.Windows.Media;

namespace HappiNESs
{
    /// <summary>
    /// An <see cref="OpenGL"/> extesions methods
    /// </summary>
    internal static class OpenGLExtension
    {
        /// <summary>
        /// Creates a rectangle with the top-left and bottom-right corners
        /// </summary>
        /// <param name="GL">The <see cref="OpenGL"/> instance</param>
        /// <param name="x1">The top-left x value</param>
        /// <param name="y1">the top-left y value</param>
        /// <param name="x2">The bottom-right x value</param>
        /// <param name="y2">The bottom-right y value</param>
        /// <param name="color">The draw color</param>
        /// <param name="z">The z-plane position, defaults to 1.0f</param>
        public static void Rectangle(this OpenGL GL, float x1, float y1, float x2, float y2, Color color = new Color(), float z = 1.0f)
        {
            // TODO: Adjust this code

            GL.Begin(OpenGL.GL_QUADS);
            
            GL.Color(color.R, color.G, color.B);

            GL.Vertex(x2, y1, z);
            GL.Vertex(x1, y1, z);
            GL.Vertex(x1, y2, z);
            GL.Vertex(x2, y2, z);

            GL.End();
        }
    }
}
