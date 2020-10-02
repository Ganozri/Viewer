using Medusa.Analyze1553B.VM;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for MTView1553.xaml
    /// </summary>
    public partial class MTView1553 : CommonPageUserControl
    {
        public MTView1553()
        {
            InitializeComponent();
        }

        private void MainChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            //
            //var x = chartPoint.X;
            //TB.Text = x.ToString();
            //IPageViewModel y = (IPageViewModel)DataContext;
            //y.CurrentRow = (int)x;
            //MessageBox.Show(x.ToString());
        }
    }
}
