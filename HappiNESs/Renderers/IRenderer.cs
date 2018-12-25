namespace HappiNESs
{
    /// <summary>
    /// A interface class for renderers types
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// The renderer name
        /// </summary>
        string RendererName { get; }

        /// <summary>
        /// Draws on the screen
        /// </summary>
        void Draw();

        /// <summary>
        /// Initialize the rendering
        /// </summary>
        void Initialize();
    }
}
