using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BottomControlButtons.xaml
    /// </summary>
    public partial class BottomControlButtons : UserControl
    {
        public BottomControlButtons()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            //Regex regex = new Regex(@"[^\s]{6,}$");
            //e.Handled = regex.IsMatch(e.Text);

            //TextBox textBox = (TextBox)sender;
            //textBox.Text.All(c => !char.IsWhiteSpace(c));
            //sender = textBox;

            //textBox.Text = Int32.Parse(textBox.Text).ToString();


            //MessageBox.Show(textBox.Text);
        }
    }
}
