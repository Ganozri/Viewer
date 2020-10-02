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
         public List<Rule> Rules {get;set;}

         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cw = (ControlWord)value;


            Rules = new List<Rule>
            {
                new Rule(31, DataDirection.R, 31, 17, "ССД", "Green"),
                new Rule(31, DataDirection.R, 29, 5, "Время", "Blue"),
                new Rule(1, DataDirection.T, 1, 32, "Время", "Blue")
            };

            foreach (var rule in Rules)
            {
                if (rule.Address == cw.Address
                    && rule.Direction == cw.Direction
                    && rule.Subaddress == cw.Subaddress
                    && rule.Length == cw.Length)
                {
                    return rule.Name;
                }
            }

            return "Null";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
