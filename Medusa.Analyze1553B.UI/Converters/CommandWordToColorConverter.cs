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
    public class CommandWordToColorConverter : IValueConverter
    {
         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = (DataRecord[])value;

            return "Red";
            //if (x.Cw1.Length==32)
            //{
            //    return "Blue";
            //}
            //else
            //{
            //     return "Red";
            //}
           
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
