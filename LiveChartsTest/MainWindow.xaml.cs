using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiveChartsTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Random rnd = new Random();

            var values = new List<int>(); 
            for (int i = 0; i < 10; i++)
            {
                values.Add(rnd.Next(0, 10));
            }
            //
           var dapperMapper = new CartesianMapper<int>()
           //the data point will be displayed at the position of its index on the X axis
           .X((value, index) => index)
           //the data point will have a Y value of its value (your double) aka the column height
           .Y((value) => value)
           //pass any Func to determine the fill color according to value and index
           //in this case, all columns over 3 height will be pink
           //in your case, you want this to depend on the index
           .Fill((value, index) => (value > 3 ? Brushes.Red : Brushes.YellowGreen));
            //
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Test data",
                    //Values = new ChartValues<double> { 10, 50, 39, 50 }
                    Values = values.AsGearedValues(),
                    Configuration = dapperMapper,
                    Fill = Brushes.BlueViolet,
                    ColumnPadding = 0.25
                }
            };

         

            //DataContext = this;

        }

        private void CartesianChart_Initialized(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                 SeriesCollection[0].Values.Add(rnd.Next(0, 10));
            }
           
            
        }
    }
}


