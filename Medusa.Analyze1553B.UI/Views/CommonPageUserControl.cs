using LiveCharts;
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

        public void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);
        }

        public void Chart_OnDataClick()
        {
            //MessageBox.Show("you clicked (" + point.X + "," + point.Y + ")");
            MessageBox.Show("Chart_OnDataClick");
            // point.Instance contains the value as object, in case you passed a class, or any other type
        }


    }
}
