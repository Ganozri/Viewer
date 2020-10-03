using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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

namespace Medusa.Analyze1553B.UI.Views
{

     public class CommandWordToColorConverter : IValueConverter
    {
        public List<Rule>  Rules = new List<Rule>
            {
                new Rule(31, DataDirection.R, 31, 17, "ССД",   "Green"),
                new Rule(31, DataDirection.R, 29, 5,  "Время", "Blue"),
                new Rule(1,  DataDirection.T, 1,  32, "Исправность устройства", "Blue")
            };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cw = (ControlWord)value;

            foreach (var rule in Rules)
            {
                if (rule.Address == cw.Address
                    && rule.Direction == cw.Direction
                    && rule.Subaddress == cw.Subaddress
                    && rule.Length == cw.Length)
                {
                    return rule.Color;
                }
            }
            return "White";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    public partial class TcpServerControl : CommonPageUserControl
    { 
       
        public TcpServerControl()
        {
            InitializeComponent();

            Style style = new Style
            {
                TargetType = typeof(ListViewItem),
                BasedOn = myListView.ItemContainerStyle
            };

            CommandWordToColorConverter commandWordToColorConverter = new CommandWordToColorConverter();
            Binding binding = new Binding("Cw1")
            {
                Converter = commandWordToColorConverter
            };
            style.Setters.Add(new Setter(BackgroundProperty, binding));

            myListView.ItemContainerStyle = style;

        }

        private void MainChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var x = chartPoint.X;

            IPageViewModel y = (IPageViewModel)DataContext;
            y.CurrentRow = (int)x;

          
        }

    }
}
