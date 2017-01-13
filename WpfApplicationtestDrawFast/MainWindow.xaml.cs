using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApplicationtestDrawFast
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Random random = new Random();

            //Color color;
            //while (true)
            //{
            //SurfaceGrid.Children.Clear();
            //color = new Color();
            //color.A = (byte)255;
            //color.R = (byte)random.Next(200);
            //color.G = (byte)random.Next(200);
            //color.B = (byte)random.Next(200);

            //for (int i = 0; i < this.Width / 100; i++)
            //{
            //    for (int j = 0; j < this.Height / 100; j++)
            //    {
            //        Rectangle rect = new Rectangle();
            //        rect.Width = 100;
            //        rect.Height = 100;
            //        rect.Fill = new SolidColorBrush(color);
            //        Canvas.SetLeft(rect, i * 100);
            //        Canvas.SetTop(rect, j * 100);

            //        SurfaceGrid.Children.Add(rect);
            //    }
            //}

            //Thread.Sleep(1000);
            //}
        }
    }
}
