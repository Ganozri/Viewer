using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Medusa.Analyze1553B.UI.Views;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VM;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Medusa.Analyze1553B.UI
{
    //Расширение для удобного добавления элементов в DataGrid
    //Grid grid = new Grid();
    //grid.SetGridChildren(UIElement,0,0);
    public static class GridExtension
    {
        public static void SetGridChildren(this Grid grid, UIElement element, int Row = 0, int Column = 0)
        {
            var Children = grid.Children;
            
            Grid.SetColumn(element, Column);
            Grid.SetRow(element,Row);
            Children.Add(element);
        }
    }
    public partial class MainWindow : MetroWindow, ISynchronizationContextProvider, IDialogService
    {
        private void DataRecordsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((StartVmObject)this.DataContext).SelectedViewModel.SelectedItem;

            var grid = (Grid)sender;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.1, GridUnitType.Star) });

            DataGrid dataGrid = new DataGrid();


            dataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            dataGrid.ItemsSource = selectedItem.ReadableDataRecords;
            dataGrid.IsReadOnly = true;
            dataGrid.CanUserSortColumns = false;
            dataGrid.Columns.Clear();

            Binding mySelectedIndexbinding = new Binding("SelectedDataRecordIndex");
            Binding mySelectedDataRecord = new Binding("SelectedDataRecord");

            dataGrid.SetBinding(DataGrid.SelectedItemProperty, mySelectedDataRecord);
            dataGrid.SetBinding(DataGrid.SelectedIndexProperty, mySelectedIndexbinding);

            TreeView treeView = new TreeView();
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
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = item.Name;
                    treeItem.IsExpanded = true;
                    if (!item.PropertyType.IsEnum)
                    {
                        PropertyInfo[] nestedProperty = GetPropertiesFromType(item.PropertyType);
                        foreach (var property in nestedProperty)
                        {
                            TreeViewItem treeViewItem = new TreeViewItem();
                            Binding myBind = new Binding("SelectedDataRecord" + "." + item.Name + "." + property.Name);
                            
                            treeViewItem.SetBinding(TreeViewItem.HeaderProperty, myBind);
                            treeViewItem.HeaderStringFormat = property.Name + " = {0}";
                            treeItem.Items.Add(treeViewItem);
                        }
                        treeView.Items.Add(treeItem);   
                    }
                }
            }
            
            grid.SetGridChildren(dataGrid, 0, 0);
            grid.SetGridChildren(treeView, 0, 1);
        }

        static PropertyInfo[] GetPropertiesFromType(Type type)
        {
            PropertyInfo[] nestedProperty = type.GetProperties();
            return nestedProperty;
        }
        static void SetAndAddColumnToDataGrid(string stringToHeader, string stringToBinding, DataGrid dataGrid)
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
        public void CreateChoosePageViewModelControl(object pageViewModel)
        {
            ChoosePageView choosePageView = new ChoosePageView();
            choosePageView.DataContext = (IPageViewModel)pageViewModel;
            choosePageView.Show();
        }
        private async void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("This is the title", "Button clicked!");
            
        }

        private async void GetHelpClick(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Help", "Вам обязательно помогут!" +
                                               "\nНо не сейчас" +
                                               "\nИ возможно не вам");
        }

        public MemoryStream ShowOpenFileDialog()
        {
            var openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = this.Filter
            };
            if (openFileDlg.ShowDialog() == true)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(openFileDlg.FileName);
                MemoryStream stream = new MemoryStream(byteArray);
                return stream;
            }
            else
            {
                return new MemoryStream();
            }
        }

        public MemoryStream ShowSaveFileDialog()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = this.Filter
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(openFileDlg.FileName);
                MemoryStream stream = new MemoryStream(byteArray);
                return stream;
            }
            else
            {
                return new MemoryStream();
            }
        }

        public void AddText(object arg, string text)
        {
            TextBlock textBlock = new TextBlock();
            if (arg != null)
            {
                if (Object.ReferenceEquals(arg.GetType(), textBlock.GetType()))
                {
                    textBlock = (TextBlock)arg;
                    textBlock.Text += text + "\n";
                }
            }

        }

        public void CreateCustomWindow()
        {
            MessageBox.Show("Здесь будет кастомное окно!");
        }

        public void ShowMessage(string message, string title = null)
        {
            MessageBox.Show(this, message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ScrollIntoView(object arg, int position)
        {
            ListView myListView = new ListView();
            if (Object.ReferenceEquals(arg.GetType(), myListView.GetType()))
            {
                myListView = (ListView)arg;
                myListView.SelectedItem = myListView.Items.GetItemAt(position);
                myListView.ScrollIntoView(myListView.SelectedItem);
                ListViewItem item = myListView.ItemContainerGenerator.ContainerFromItem(myListView.SelectedItem) as ListViewItem;
                item.Focus();
            }
            else
            {

            }

        }

        public void UpdateView(object arg)
        {
            ListView myListView = new ListView();
            if (Object.ReferenceEquals(arg.GetType(), myListView.GetType()))
            {
                myListView = (ListView)arg;
            }
        }

        public int CurrentPosition(object arg)
        {
            ListView myListView = (ListView)arg;
            if (Object.ReferenceEquals(arg.GetType(), myListView.GetType()))
            {
                int position = myListView.SelectedIndex;
                return position;
            }
            else
            {
                return 0;
            }

        }
    }
}
