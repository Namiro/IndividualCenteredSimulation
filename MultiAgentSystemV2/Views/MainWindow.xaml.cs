using System.Windows;

namespace MultiAgentSystem.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.LoadSettings();

            this.Height = App.CanvasSizeY + 37;
            this.Width = App.CanvasSizeX + 14 + 200;

            InitializeComponent();
        }
    }
}
