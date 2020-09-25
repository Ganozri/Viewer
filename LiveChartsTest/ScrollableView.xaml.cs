using System;
using LiveCharts.Events;

namespace LiveChartsTest
{
    /// <summary>
    /// Interaction logic for ScrollingWindow.xaml
    /// </summary>
    public partial class ScrollableView : IDisposable
    {
        public ScrollableView()
        {
            InitializeComponent();
        }

    
        public void Dispose()
        {
            var vm = (ScrollableViewModel)DataContext;
            vm.Values.Dispose();
        }
    }
}