using DynamicData;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Medusa.Analyze1553B.UI.Converters
{
    //[ValueConversion(typeof(DataRecord[]),typeof(double[]))]
    public class DataRecordsToLengthListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Series = new SeriesCollection();

            int count = 150;
            var ArrayOfLength = new int[count];
            var x = (IPageViewModel)value;
            var CurrentRow = x.CurrentRow;

            CartesianMapper<int> mapper = Mappers.Xy<int>()
                .X((value, index) => index + CurrentRow)
                .Y(value => value)
                .Fill((value, index) => value > 5 ? Brushes.Red : Brushes.Blue);


            Random rnd = new Random();
            for (int i = 0; i < (count - 1); i++)
            {
                ArrayOfLength[i] = rnd.Next(0,10);
                //ArrayOfLength[i] = x.DataRecordsList[i+CurrentRow].Cw1.Length;
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

            return Series;
 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
