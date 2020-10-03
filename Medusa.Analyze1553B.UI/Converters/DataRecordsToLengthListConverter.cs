using DynamicData;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using Medusa.Analyze1553B.Common;
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

        public double[] ArrayOfLength;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var x = (DataRecord[])value;
            //if (value!=null && x.Length > 0)
            //{
            //ArrayOfLength = new double[x.Length];
            //for (int i = 0; i < x.Length - 1; i++)
            //{
            //    ArrayOfLength[i] = x[i].Cw1.Length;
              
            //}
            //var Values = ArrayOfLength.AsGearedValues();

            //Values.WithQuality(Quality.Low);

            //return Values;    
            //}
            //else
            //{
            //    return null;
            //}
            var Series = new SeriesCollection();

            var r = new Random();
            var Values = new ChartValues<ObservableValue>
            {
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400))
            };

             var Mapper = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill(item => (item.Value > 200 ? Brushes.Red : null)   );
                //.Stroke(item => item.Value > 200 ? Brushes.AliceBlue : null);


            var series = new ColumnSeries
                {
                    Title = "Population of Bodrum",
                    Values = new ChartValues<double> { 1500, 2500, 3700, 2000, 1000 },
                    Fill = Brushes.Blue,
                    ColumnPadding = 0
                    //Fill = Brushes.Transparent,
                    //Foreground = Brushes.White,
                    //Configuration = Mapper,
                    //Fill = Brushes.Red
                    //PointForeground=Brushes.White,
                    //PointGeometry = null //use a null geometry when you have many series
                };

            CartesianMapper<double> mapper = Mappers.Xy<double>()
                .X((value, index) => index)
                .Y(value => value)
                .Fill((value, index) => value>2000 ? Brushes.Red : Brushes.Blue);

            series.Configuration = mapper;

            Series.Add(series);
            return Series;
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
