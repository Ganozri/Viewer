using System.Windows.Controls;


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

    }
}
