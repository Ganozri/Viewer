using Medusa.Analyze1553B.UIServices;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SynchronizationContext = System.Threading.SynchronizationContext;

namespace Medusa.Analyze1553B.UI
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ISynchronizationContextProvider, IDialogService
    {
        readonly Microsoft.Win32.OpenFileDialog openFileDlg;
        readonly Microsoft.Win32.SaveFileDialog saveFileDialog;

        public string Filter { get; set; }

        public SynchronizationContext SynchronizationContext { get; }

        public MainWindow()
        {
            InitializeComponent();
            PreviewMouseWheel += Window_PreviewMouseWheel;

            SynchronizationContext = SynchronizationContext.Current;

            if (Filter==null)
            {
                this.Filter += "All files (*.*)|*.*";
            }
            else
            {
                this.Filter += "|All files (*.*)|*.*";
            }
           
            openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = this.Filter
            };
            saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = this.Filter
            };
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;

            if (e.Delta > 0)
                ScaleUI.Value += 0.05;

            else if (e.Delta < 0)
                ScaleUI.Value -= 0.05;
        }

        public MemoryStream ShowOpenFileDialog()
        {
            var openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = this.Filter
            };
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
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = this.Filter
            };
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
            if (arg!=null)
            {
                if (Object.ReferenceEquals(arg.GetType(), textBlock.GetType()))
                {
                    textBlock = (TextBlock)arg;
                    textBlock.Text += text + "\n";
                }
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

        public int IndexSelectedViewModel()
        {
            return products.SelectedIndex;
        }

    
    }
}
