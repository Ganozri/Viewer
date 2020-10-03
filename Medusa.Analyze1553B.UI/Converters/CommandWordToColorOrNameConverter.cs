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
     public class CommandWordToColorOrNameConverter : IValueConverter
    {
        public List<Rule>  Rules {get;set;}

        public CommandWordToColorOrNameConverter(List<Rule>  Rules)
        {
            this.Rules = Rules;
        }

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
                     return ((string)parameter=="Color") ? rule.Color : rule.Name;
                }
            }

            return ((string)parameter=="Color") ? "White" : "Unknown";     
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
