using System.Windows;

namespace MultiAgentSystemV2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.LoadSettings();

            this.Height = App.CanvasSizeY + 35;
            this.Width = App.CanvasSizeX + 13;

            InitializeComponent();
        }
    }
}
