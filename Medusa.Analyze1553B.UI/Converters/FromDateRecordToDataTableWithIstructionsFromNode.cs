using DynamicData;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Medusa.Analyze1553B.UI.Converters
{
    public class FromDateRecordToDataTableWithIstructionsFromNode : IValueConverter
    {
        [Reactive] public DataTable Table { get;set;}
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inputItem = (Item)value;
            //var inputItem = (ReadOnlyObservableCollection<DataRecord>)value;

            //Table = new DataTable();

            //Table.Columns.Add("Type", typeof(string));
            //Table.Columns.Add("MonitorTime", typeof(string));
            //Table.Columns.Add("Cl", typeof(string));
            //Table.Columns.Add("Error", typeof(string));
            //Table.Columns.Add("Cw1", typeof(string));
            //Table.Columns.Add("Addr", typeof(string));
            //Table.Columns.Add("Dir", typeof(string));
            //Table.Columns.Add("Sub", typeof(string));
            //Table.Columns.Add("Length", typeof(string));
            //Table.Columns.Add("Cw2", typeof(string));
            //Table.Columns.Add("Rw1", typeof(string));
            //Table.Columns.Add("Rw2", typeof(string));

            //Table.Columns.Add("0", typeof(string));
            //Table.Columns.Add("1", typeof(string));
            //Table.Columns.Add("2", typeof(string));
            //Table.Columns.Add("3", typeof(string));
            //Table.Columns.Add("4", typeof(string));
            //Table.Columns.Add("5", typeof(string));
            //Table.Columns.Add("6", typeof(string));
            //Table.Columns.Add("7", typeof(string));
            //Table.Columns.Add("8", typeof(string));
            //Table.Columns.Add("9", typeof(string));
            //Table.Columns.Add("10", typeof(string));

            //Table.Columns.Add("11", typeof(string));
            //Table.Columns.Add("12", typeof(string));
            //Table.Columns.Add("13", typeof(string));
            //Table.Columns.Add("14", typeof(string));
            //Table.Columns.Add("15", typeof(string));
            //Table.Columns.Add("16", typeof(string));
            //Table.Columns.Add("17", typeof(string));
            //Table.Columns.Add("18", typeof(string));
            //Table.Columns.Add("19", typeof(string));
            //Table.Columns.Add("20", typeof(string));

            //Table.Columns.Add("21", typeof(string));
            //Table.Columns.Add("22", typeof(string));
            //Table.Columns.Add("23", typeof(string));
            //Table.Columns.Add("24", typeof(string));
            //Table.Columns.Add("25", typeof(string));
            //Table.Columns.Add("26", typeof(string));
            //Table.Columns.Add("27", typeof(string));
            //Table.Columns.Add("28", typeof(string));
            //Table.Columns.Add("29", typeof(string));
            //Table.Columns.Add("30", typeof(string));

            //Table.Columns.Add("31", typeof(string));

            //int count = 0;
            //foreach (var record in inputItem.DataRecords)
            ////foreach (var record in inputItem)
            //{

            //    Table.Rows.Add(
            //        "",
            //        record.MonitorTime,
            //        record.Channel,
            //        record.Error,
            //        String.Format("{0:X4}", record.Cw1.Value),
            //        record.Cw1.Address,
            //        record.Cw1.Direction,
            //        record.Cw1.Subaddress,
            //        record.Cw1.Length,

            //        record.Cw2.Value.Value,
            //        record.Rw1.Value.Value,
            //        record.Rw2.Value.Value);

            //    //foreach (var rule in Rules)
            //    //{
            //    //    if (rule.Address == record.Cw1.Address
            //    //        && rule.Direction == record.Cw1.Direction
            //    //        && rule.Subaddress == record.Cw1.Subaddress
            //    //        && rule.Length == record.Cw1.Length)
            //    //    {
            //    //        Table.Rows[count][0] = rule.Name;
            //    //    }
            //    //}

            //    for (int i = 12; i < 12 + record.Data.Length; i++)
            //    {
            //        Table.Rows[count][i] = String.Format("{0:X4}", record.Data[i - 12]);


            //    }
            //    count++;



            //}

            //return Table;
            
          



            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
