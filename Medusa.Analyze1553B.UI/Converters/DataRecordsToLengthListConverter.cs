using DynamicData;
using LiveCharts.Geared;
using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Medusa.Analyze1553B.UI.Converters
{
    //[ValueConversion(typeof(DataRecord[]),typeof(double[]))]
    public class DataRecordsToLengthListConverter : IValueConverter
    {

        public double[] ArrayOfLength;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = (DataRecord[])value;
            if (value!=null && x.Length > 0)
            {
            ArrayOfLength = new double[x.Length];
            for (int i = 0; i < x.Length - 1; i++)
            {
                ArrayOfLength[i] = x[i].Cw1.Length;
              
            }
            var Values = ArrayOfLength.AsGearedValues();

            Values.WithQuality(Quality.Low);

            return Values;    
            }
            else
            {
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
