using System.Windows.Controls;

namespace HappiNESs
{
    /// <summary>
    /// Interação lógica para MenuStripControl.xam
    /// </summary>
    public partial class MenuStripControl : UserControl
    {
        public MenuStripControl()
        {
            InitializeComponent();

            // Bind view model
            DataContext = new MenuStripControlViewModel();
        }
    }
}
