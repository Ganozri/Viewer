using System;
using System.Collections.Generic;
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

namespace Medusa.Analyze1553B.UI.Controls
{
    /// <summary>
    /// Interaction logic for ExpanderWithBottomControlButtons.xaml
    /// </summary>
    public partial class ExpanderWithBottomControlButtons : UserControl
    {

        public ExpanderWithBottomControlButtons()
        {
            InitializeComponent();
        }

        private void Expander_MouseEnter(object sender, MouseEventArgs e)
        {
            Expander expander = (Expander)sender;
            expander.IsExpanded = true;
            PanelWithNumbers.Visibility = Visibility.Hidden;
        }

        private void Expander_MouseLeave(object sender, MouseEventArgs e)
        {
            Expander expander = (Expander)sender;
            expander.IsExpanded = false;
            PanelWithNumbers.Visibility = Visibility.Visible;
        }
    }
}
