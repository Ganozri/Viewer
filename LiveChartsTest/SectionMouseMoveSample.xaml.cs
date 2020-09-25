using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace LiveChartsTest
{
    public partial class SectionMouseMoveSample : UserControl
    {
        public SectionMouseMoveSample()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            //var vm = (ViewModel)DataContext;
            //var chart = (LiveCharts.Wpf.CartesianChart)sender;

            ////lets get where the mouse is at our chart
            //var mouseCoordinate = e.GetPosition(chart);

            ////now that we know where the mouse is, lets use
            ////ConverToChartValues extension
            ////it takes a point in pixes and scales it to our chart current scale/values
            //var p = chart.ConvertToChartValues(mouseCoordinate);

            ////in the Y section, lets use the raw value
            //vm.YPointer = p.Y;

            ////for X in this case we will only highlight the closest point.
            ////lets use the already defined ClosestPointTo extension
            ////it will return the closest ChartPoint to a value according to an axis.
            ////here we get the closest point to p.X according to the X axis
            //var series = chart.Series[0];
            //var closetsPoint = series.ClosestPointTo(p.X, AxisOrientation.X);

            //vm.XPointer = closetsPoint.X;
            //MessageBox.Show(closetsPoint.X.ToString());
        }

        private void CartesianChart_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var chart = (LiveCharts.Wpf.CartesianChart)sender;

            //lets get where the mouse is at our chart
            var mouseCoordinate = e.GetPosition(chart);

            //now that we know where the mouse is, lets use
            //ConverToChartValues extension
            //it takes a point in pixes and scales it to our chart current scale/values
            var p = chart.ConvertToChartValues(mouseCoordinate);

            //var series = chart.Series[0];
            //var closetsPoint = series.ClosestPointTo(p.X, AxisOrientation.X);

            var point = p.X;
            MessageBox.Show(point.ToString());
            //MessageBox.Show("CartesianChart_PreviewMouseLeftButtonDown");
        }

        private void CartesianChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var chart = (LiveCharts.Wpf.CartesianChart)sender;

            ////lets get where the mouse is at our chart
            //var mouseCoordinate = e.GetPosition(chart);

            ////now that we know where the mouse is, lets use
            ////ConverToChartValues extension
            ////it takes a point in pixes and scales it to our chart current scale/values
            //var p = chart.ConvertToChartValues(mouseCoordinate);

            ////var series = chart.Series[0];
            ////var closetsPoint = series.ClosestPointTo(p.X, AxisOrientation.X);

            //var point = p.X;
            //MessageBox.Show(point.ToString());
           
        }

        private void CartesianChart_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("CartesianChart_FocusableChanged");
        }

        private void CartesianChart_GotFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CartesianChart_FocusableChanged");
        }

        private void CartesianChart_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("CartesianChart_PreviewMouseUp");
        }
    }
}
