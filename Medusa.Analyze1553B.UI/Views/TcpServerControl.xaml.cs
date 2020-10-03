using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UI.Converters;
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

            List<Rule>  Rules = new List<Rule>
            {
                new Rule(31, DataDirection.R, 31, 17, "ССД", "Green"),
                new Rule(31, DataDirection.R, 29, 5,  "Время", "Blue"),
                new Rule(1,  DataDirection.T, 1, 32,  "Исправность устройства", "Blue")
            };
            CommandWordToColorOrNameConverter commandWordToColorConverter = new CommandWordToColorOrNameConverter(Rules);

            Binding binding = new Binding("Cw1")
            {
                Converter = commandWordToColorConverter,
                ConverterParameter = "Color"
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
