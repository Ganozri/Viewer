using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VM;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SynchronizationContext = System.Threading.SynchronizationContext;

namespace Program.ByteSumCountingProgram.UI
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, ISynchronizationContextProvider, IDialogService
    {
        readonly Microsoft.Win32.OpenFileDialog openFileDlg;
        readonly Microsoft.Win32.SaveFileDialog saveFileDialog;
        public string Filter { get; set; }

        public SynchronizationContext SynchronizationContext { get; }

        public MainWindow()
        {
            InitializeComponent();
          
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

    }

}
