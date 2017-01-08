using IndividualCenteredSimulation.ViewModels;
using System.Windows;

namespace IndividualCenteredSimulation.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.LoadSettings();

            this.Height = App.CanvasSizeY;
            this.Width = App.CanvasSizeX;

            InitializeComponent();

            MainWindowViewModel.Initialize(GraphicHelperGridSharpDX);
        }
    }
}
