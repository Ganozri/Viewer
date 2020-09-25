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
        //
        public GearedValues<double> Values { get; set; }
        public CommonPageUserControl()
        {
            int x = 180000;
            double[] arr = new double[] { 1, 1, 6, 5, 4, 32, 32, 32, 32, 32, 32, 1, 1, 6, 6, 6, 8, 12 };

            double[] d = new double[x];

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    d[j + (i * (arr.Length - 1))] = arr[j];
                }
            }

            Values = d.AsGearedValues();
            for (int i = 0; i < 100; i++)
            {
                Values.Add(32);
            }

            Values.WithQuality(Quality.Low);
        }
        //
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
