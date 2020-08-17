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
    public partial class TcpServerControl : UserControl
    {
        public TcpServerControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //OutputWriteLine("Button_Click");
        }

        private void output_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            //outputScroll.ScrollToEnd();
        }

        private void currentTagNotContactsList_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
           
        }

        private void myListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            MessageBox.Show("myListView_ScrollChanged");
        }
    }
}
