using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UI.Converters;
using Medusa.Analyze1553B.VM;
using Medusa.Analyze1553B.VM.ProductViewModels;
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
                new Rule(31, DataDirection.R, 31, 17, "ССД",                    "Green"),//Change by string
                new Rule(31, DataDirection.R, 29, 5,  "Время",                  Colors.BlueViolet.ToString()),//Change by Colors
                new Rule(1,  DataDirection.T, 29, 10,  "Время(полученное)",     Colors.LightCoral.ToString()),//Change by Colors as string
                new Rule(1,  DataDirection.T, 1, 32,  "Исправность устройства", "#3459d0"),//Change by string in hex
                new Rule(1,  DataDirection.R, 1, 32,  "Управление терминалом",  Colors.Azure),
                new Rule(31, DataDirection.R, 7, 32,  "Навигация и эл. орбиты", Colors.AliceBlue),
                new Rule(31, DataDirection.R, 10, 19,  "Инф. о времени эксп-я", Colors.AntiqueWhite),
                new Rule(1,  DataDirection.T, 2, 32,  "Рег. ТМ-1",              Colors.Aqua),
                new Rule(1,  DataDirection.T, 3, 32,  "Рег. ТМ-2",              Colors.Aquamarine),
                new Rule(1,  DataDirection.T, 4, 32,  "Рег. ТМ-3",              Colors.Azure),
                new Rule(1,  DataDirection.T, 5, 32,  "Рег. ТМ-4",              Colors.BlanchedAlmond),
                new Rule(1,  DataDirection.T, 6, 32,  "Данные УФПО",            Colors.MediumAquamarine),
                new Rule(1,  DataDirection.T, 7, 32,  "Ориентация опт. осей",   Colors.Transparent),
                new Rule(1,  DataDirection.T, 9, 32,  "Ориентация ОЭЗА",        Colors.DimGray),
                new Rule(1,  DataDirection.T, 10, 29,  "Ориентация ТК",         Colors.LightGray),
                new Rule(1,  DataDirection.T, 8, 32,  "DBG TM",                 Colors.Beige),
                new Rule(1,  DataDirection.R, 27, 2,  "DTD",                    Colors.Bisque),
                new Rule(1,  DataDirection.T, 27, 2,  "DTC",                    Colors.Brown),
                new Rule(1,  DataDirection.R, 11, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 12, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 13, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 14, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 15, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 16, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 17, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 18, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 19, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 20, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 21, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 22, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 23, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.R, 24, 8,  "DDB",                    Colors.BurlyWood),
                new Rule(1,  DataDirection.T, 28, 2,  "ATR",                    Colors.CadetBlue),
                new Rule(1,  DataDirection.R, 28, 2,  "ATC",                    Colors.Chartreuse),
                new Rule(1,  DataDirection.T, 11, 9,  "ADB",                    Colors.Chocolate)
            };
            
            CommandWordToColorOrNameConverter commandWordToColorConverter = new CommandWordToColorOrNameConverter(Rules);

            Binding bindingName = new Binding("Cw1")
            {
                Converter = commandWordToColorConverter,
                ConverterParameter = "Name"
            };
            TypeName.DisplayMemberBinding = bindingName;

            Binding binding = new Binding("Cw1")
            {
                Converter = commandWordToColorConverter,
                ConverterParameter = "Color"
            };
            style.Setters.Add(new Setter(BackgroundProperty, binding));
            myListView.ItemContainerStyle = style;

           
            //<!--Series="{Binding DataRecordsList,
            //                                   Converter={StaticResource MyDataRecordsToLengthListConverter} }"-->
            //
           
        }

        private void MainChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            int pointX = (int)chartPoint.X;
            ((IPageViewModel)DataContext).CurrentRow = pointX;
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
            ListView listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);
            //
            IPageViewModel x = (IPageViewModel)DataContext;
            //MessageBox.Show(x.CurrentRow.ToString());
            var Series = new SeriesCollection();

            int count = 150;
            var ArrayOfLength = new int[count];
            var CurrentRow = x.CurrentRow;

            CartesianMapper<int> mapper = Mappers.Xy<int>()
                .X((value, index) => index + CurrentRow)
                .Y(value => value)
                .Fill((value, index) => value > 5 ? Brushes.Red : Brushes.Blue);


            Random rnd = new Random();
            for (int i = 0; i < (count - 1); i++)
            {
                //ArrayOfLength[i] = rnd.Next(0, 10);
                ArrayOfLength[i] = x.DataRecordsList[i+CurrentRow].Cw1.Length;
            }

            var values = ArrayOfLength.AsChartValues();
            var series = new ColumnSeries
            {
                //Title = "Data",
                Title = x.Name,
                Values = values,
                Configuration = mapper,
                ColumnPadding = 0.25,
                Fill = Brushes.Blue
            };

            Series.Add(series);

            MainChart.Series = Series;

        }
    }
}
