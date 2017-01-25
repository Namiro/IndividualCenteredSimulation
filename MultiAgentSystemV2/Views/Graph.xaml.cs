using LiveCharts;
using LiveCharts.Wpf;
using MultiAgentSystem;
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
using System.Windows.Shapes;

namespace MultiAgentSystemV2.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        public static SeriesCollection SeriesCollection { get; set; }
        public static List<int> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Graph()
        {
            InitializeComponent();


            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Sharks",
                    Values = new ChartValues<double> { App.SharksNumber }
                },
                new LineSeries
                {
                    Title = "Fishes",
                    Values = new ChartValues<double> { App.FishsNumber }
                }
            };

            Labels =  new List<int> { 0 };
            YFormatter = value => value.ToString("");

            DataContext = this;
        }

        
    }
}
