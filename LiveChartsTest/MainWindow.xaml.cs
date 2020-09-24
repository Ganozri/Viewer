using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
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

        public MainWindow()
        {
            InitializeComponent();



            int x = 10000;
            double[] d = new double[x];
            for (int i = 0; i < x; i++)
            {
                d[i] = i;
            }

            Values = d.AsGearedValues();

            Values.WithQuality(Quality.Low);
            DataContext = this;
        }

    public GearedValues<double> Values { get; set; }
}
}


