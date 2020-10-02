using LiveCharts;
using LiveCharts.Geared;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Medusa.Analyze1553B.UI
{
    /// <summary>
    /// Interaction logic for CommonPageUserControl.xaml
    /// </summary>
    public partial class CommonPageUserControl : UserControl
    {
        public CommonPageUserControl()
        {
           
        }

        public void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);
         
        }

    }
}
