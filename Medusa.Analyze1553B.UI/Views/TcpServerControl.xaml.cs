using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medusa.Analyze1553B.UI.Views
{
    /// <summary>
    /// Interaction logic for TcpServerControl.xaml
    /// </summary>
    /// 

    public partial class TcpServerControl : CommonPageUserControl
    { 

        public TcpServerControl()
        {
            InitializeComponent();
        }

        private void MainChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var x = chartPoint.X;

            IPageViewModel y = (IPageViewModel)DataContext;
            y.CurrentRow = (int)x;
;
        }

    }
}
