using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
namespace Medusa.Analyze1553B.UI.Controls
{
    /// <summary>
    /// Interaction logic for DataRecordsList.xaml
    /// </summary>
    public partial class DataRecordsList : CommonPageUserControl
    {

        public DataRecordsList()
        {
            InitializeComponent();  
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var x = this.DataContext;
            MessageBox.Show(x.ToString());

        }

    }
}
