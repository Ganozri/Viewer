using Medusa.Analyze1553B.UI.Views;
using Medusa.Analyze1553B.UIServices;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SynchronizationContext = System.Threading.SynchronizationContext;
//

//
namespace Medusa.Analyze1553B.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ISynchronizationContextProvider, IDialogService
    {
        public string test { get; set; } = "test";
        readonly Microsoft.Win32.OpenFileDialog openFileDlg;
        readonly Microsoft.Win32.SaveFileDialog saveFileDialog;

        string Filter = "BMD files (*.bmd)|*.bmd|Xml files (*.xml)|*.xml|All files (*.*)|*.*";
        //

        //
        public SynchronizationContext SynchronizationContext { get; }

        public MainWindow()
        {
            InitializeComponent();
            SynchronizationContext = SynchronizationContext.Current;
            openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".bmd",
                Filter = this.Filter
            };
            saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = this.Filter
            };
        }

        public MemoryStream ShowOpenFileDialog()
        {
            if (openFileDlg.ShowDialog() == true)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(openFileDlg.FileName);
                MemoryStream stream = new MemoryStream(byteArray);
                return stream;
            }
            else
            {
                return new MemoryStream();
            }

        }

        public MemoryStream ShowSaveFileDialog()
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(openFileDlg.FileName);
                MemoryStream stream = new MemoryStream(byteArray);
                return stream;
            }
            else
            {
                return new MemoryStream();
            }
        }

        public void AddText(object arg, string text)
        {
            TextBlock textBlock = new TextBlock();
            if (Object.ReferenceEquals(arg.GetType(), textBlock.GetType()))
            {
                textBlock = (TextBlock)arg;
                textBlock.Text += text + "\n";
            }
        }

        public void CreateCustomWindow()
        {
            MessageBox.Show("Здесь будет кастомное окно!");
        }

        public void ShowMessage(string message, string title = null)
        {
            MessageBox.Show(this, message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ScrollIntoView(object arg, int position)
        {
            ListView myListView = new ListView();
            if (Object.ReferenceEquals(arg.GetType(), myListView.GetType()))
            {
                myListView = (ListView)arg;
                myListView.SelectedItem = myListView.Items.GetItemAt(position);
                myListView.ScrollIntoView(myListView.SelectedItem);
                ListViewItem item = myListView.ItemContainerGenerator.ContainerFromItem(myListView.SelectedItem) as ListViewItem;
                item.Focus();
            }
            else
            {
                
            }
            
        }

        public void UpdateView(object arg)
        {
            ListView myListView = new ListView();
            if (Object.ReferenceEquals(arg.GetType(), myListView.GetType()))
            {
                myListView = (ListView)arg;

                
                //MessageBox.Show("UpdateView!");
            }
        }

        public int CurrentPosition(object arg)
        {
            ListView myListView = (ListView)arg;
            if (Object.ReferenceEquals(arg.GetType(), myListView.GetType()))
            {
                int position = myListView.SelectedIndex;
                return position;
            }
            else
            {
                return 0;
            }
            
        }

    }
}
