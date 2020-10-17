using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VM;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SynchronizationContext = System.Threading.SynchronizationContext;

namespace Medusa.Analyze1553B.UI
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, ISynchronizationContextProvider, IDialogService
    {
        readonly Microsoft.Win32.OpenFileDialog openFileDlg;
        readonly Microsoft.Win32.SaveFileDialog saveFileDialog;
        public string Filter { get; set; }

        public SynchronizationContext SynchronizationContext { get; }

        public MainWindow()
        {
            InitializeComponent();
          
            SynchronizationContext = SynchronizationContext.Current;

            if (Filter==null)
            {
                this.Filter += "All files (*.*)|*.*";
            }
            else
            {
                this.Filter += "|All files (*.*)|*.*";
            }
           
            openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = this.Filter
            };
            saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = this.Filter
            };
        }

        private void DataRecordsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((StartVmObject)this.DataContext).SelectedViewModel.SelectedItem;
            var dataGrid = (DataGrid)sender;
            dataGrid.Columns.Clear();
            // Get the type handle of a specified class.
            // Get the properties of 'Type' class object.
            // Get the fields of the specified class.
            PropertyInfo[] myPropertyInfo = GetPropertiesFromType(selectedItem.type);

            foreach (var item in myPropertyInfo)
            {
                if (item.PropertyType.IsPrimitive)
                {
                    SetAndAddColumnToDataGrid(item.Name, item.Name, dataGrid);
                }
                else
                {
                    PropertyInfo[] nestedProperty = GetPropertiesFromType(item.PropertyType);
                    if (nestedProperty.Length == 0)
                    {
                        SetAndAddColumnToDataGrid(item.Name, item.Name, dataGrid);
                    }
                    else
                    {
                        foreach (var property in nestedProperty)
                        {
                            SetAndAddColumnToDataGrid((item.Name + "." + property.Name), (item.Name + "." + property.Name), dataGrid);
                        }
                    }
                }
            } 
        }
        static PropertyInfo[] GetPropertiesFromType(Type type)
        {
            PropertyInfo[] nestedProperty = type.GetProperties();
            return nestedProperty;
        }
        static void SetAndAddColumnToDataGrid(string stringToHeader, string stringToBinding,DataGrid dataGrid)
        {
            DataGridTextColumn col = SetColumn(stringToHeader, stringToBinding);
            dataGrid.Columns.Add(col);
        }
        static DataGridTextColumn SetColumn(string stringToHeader, string stringToBinding)
        {
            DataGridTextColumn col = new DataGridTextColumn
            {
                Header = stringToHeader,
                Binding = new Binding(stringToBinding)
            };
            return col;
        }
    }

}
