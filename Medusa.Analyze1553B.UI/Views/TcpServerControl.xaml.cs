using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UI.Converters;
using Medusa.Analyze1553B.VM;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using System.Windows.Media;


namespace Medusa.Analyze1553B.UI.Views
{

    public partial class TcpServerControl : CommonPageUserControl
    { 
        public List<Rule>  Rules {get;set;}
        public SeriesCollection Series = new SeriesCollection();
        public int PreviousCurrentRowIndex = 0;

        public TcpServerControl()
        {
            InitializeComponent();

            Style style = new Style
            {
                TargetType = typeof(ListViewItem),
                BasedOn = myListView.ItemContainerStyle
            };

            Rules = new List<Rule>
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
            
            CommandWordToColorOrNameConverter CommandWordToColorOrNameConverter = new CommandWordToColorOrNameConverter(Rules);

            Binding bindingName = new Binding("Cw1")
            {
                Converter = CommandWordToColorOrNameConverter,
                ConverterParameter = "Name"
            };
            TypeName.DisplayMemberBinding = bindingName;

            Binding binding = new Binding("Cw1")
            {
                Converter = CommandWordToColorOrNameConverter,
                ConverterParameter = "Color"
            };
            style.Setters.Add(new Setter(BackgroundProperty, binding));
            myListView.ItemContainerStyle = style;
        }

        private void MainChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            int pointX = (int)chartPoint.X;
            ((IPageViewModel)DataContext).CurrentRow = pointX;
        }

        private string GetColorByCommandWord(ControlWord cw,string parameter)
        {   
            foreach (var rule in Rules)
            {
                if (rule.Address == cw.Address
                    && rule.Direction == cw.Direction
                    && rule.Subaddress == cw.Subaddress
                    && rule.Length == cw.Length)
                {
                     return (parameter=="Color") ? rule.Color : rule.Name;
                }
            }
            return (parameter=="Color") ? "Black" : "Unknown";    
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);

            IPageViewModel x = (IPageViewModel)DataContext;
            try
            {
                var cw = x.DataRecordsList[x.CurrentRow].Cw1;
                
                int count = 100;
                
                var CurrentRow = x.CurrentRow;
    
                if (Math.Abs(PreviousCurrentRowIndex - CurrentRow) < 50 && Series.Count>0)
                {
                   
                    if (CurrentRow > PreviousCurrentRowIndex)
                    {
                        int difBetweenRow = CurrentRow - PreviousCurrentRowIndex;
                        for (int i = 0; i < difBetweenRow; i++)
                        {
                            Series[0].Values.RemoveAt(0);
                            Series[0].Values.Add(x.DataRecordsList[i+count+PreviousCurrentRowIndex].Cw1.Length);
                        }
                        CartesianMapper<int> mapper = Mappers.Xy<int>()
                                                     .X((value, index) => index + CurrentRow)
                                                     .Y(value => value)
                                                     .Fill((value, index) => (Brush)(new BrushConverter()
                                                                             .ConvertFrom(GetColorByCommandWord(x.DataRecordsList[index + CurrentRow].Cw1, "Color"))));
                        Series[0].Configuration = mapper;
                        PreviousCurrentRowIndex = CurrentRow;
                    }
                    else
                    {
                        int difBetweenRow = PreviousCurrentRowIndex-CurrentRow;
                        for (int i = 0; i < difBetweenRow; i++)
                        {
                            Series[0].Values.RemoveAt(Series[0].Values.Count-1);
                            Series[0].Values.Insert(0,x.DataRecordsList[PreviousCurrentRowIndex-i].Cw1.Length);
                        }
                        CartesianMapper<int> mapper = Mappers.Xy<int>()
                                                     .X((value, index) => index + CurrentRow)
                                                     .Y(value => value)
                                                     .Fill((value, index) => (Brush)(new BrushConverter()
                                                                             .ConvertFrom(GetColorByCommandWord(x.DataRecordsList[index + CurrentRow].Cw1, "Color"))));
                        Series[0].Configuration = mapper;
                        PreviousCurrentRowIndex = CurrentRow;
                        
                    }
                }
                else
                {
                    var ArrayOfLength = new int[count];
                    Series.Clear();

                    CartesianMapper<int> mapper = Mappers.Xy<int>()
                        .X((value, index) => index + CurrentRow)
                        .Y(value => value)
                        .Fill((value, index) => (Brush)(new BrushConverter()
                                                        .ConvertFrom(GetColorByCommandWord(x.DataRecordsList[index + CurrentRow].Cw1, "Color"))));

                    for (int i = 0; i < count; i++)
                    {
                        ArrayOfLength[i] = x.DataRecordsList[i + CurrentRow].Cw1.Length;
                    }

                    var values = ArrayOfLength.AsChartValues();
                    var series = new ColumnSeries
                    {
                        Values = values,
                        Configuration = mapper,
                        ColumnPadding = 0.25,
                        Fill = Brushes.Blue
                    };

                    Series.Add(series);
                }
                MainChart.Series = Series;
            }
            catch (Exception ex)
            {    
                System.IO.File.WriteAllText("Errors.txt", ex.Message);
            }
            
        }

       
    }
}
