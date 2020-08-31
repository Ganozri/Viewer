using System;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
using System.Xml;
using System.Text;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using ReactiveUI;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Medusa.Analyze1553B.VM.ProductViewModels;
using DynamicData;
using System.Net.Sockets;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using Medusa.Analyze1553B.Common;

namespace Medusa.Analyze1553B.VM
{
    public partial class Commands
    {
        public readonly IScheduler scheduler;
        public readonly IVmObject vmObject;
        public readonly IDialogService dialogService;
        public readonly IDataService dataService;

        public readonly ISynchronizationContextProvider syncContext;


        #region Commands
        public ICommand DoNothingCommand { get; }

        public ICommand NextCurrentRowCommand { get; }
        public ICommand PrevCurrentRowCommand { get; }

        public ICommand OpenXmlForTableCreationCommand { get; }
        public ICommand SaveXmlFromTableCommand { get; }
        public ICommand ShowHelpInformationCommand { get; }

        public ICommand UpdateCurrentRowCommand { get; }
        public ICommand AddViewModelCommand { get; }
        public ICommand RemoveViewModelCommand { get; }

        public ICommand ConnectAsTcpClientCommand { get; }
        public ICommand TestCommand { get; }

        #endregion


        public Commands(ISynchronizationContextProvider syncContext, IVmObject vmObject, IDialogService dialogService, IDataService dataService)
        {
            this.vmObject       = vmObject;
            this.dialogService  = dialogService;
            this.dataService    = dataService;
            this.scheduler      = new SynchronizationContextScheduler(syncContext.SynchronizationContext);
            this.syncContext    = syncContext;

            DoNothingCommand                = CreateCommand(DoNothing);

            NextCurrentRowCommand           = CreateCommand<object>(NextCurrentRow);
            PrevCurrentRowCommand           = CreateCommand<object>(PrevCurrentRow);
            OpenXmlForTableCreationCommand  = CreateCommand(OpenFileForDataCreation);
            SaveXmlFromTableCommand         = CreateCommand(SaveXmlFromTable);
            ShowHelpInformationCommand      = CreateCommand<object>(ShowHelpInformation);
            UpdateCurrentRowCommand         = CreateCommand<object>(UpdateCurrentRow);
            AddViewModelCommand             = CreateCommand<object>(AddViewModel);
            ConnectAsTcpClientCommand       = CreateCommand<object>(RunConnectAsTcpClient);
            TestCommand                     = CreateCommand<object>(Test);
            RemoveViewModelCommand          = CreateCommand(RemoveViewModel);
        }

        #region Command Implementation

       

        private void Test(object obj)
        {
            dialogService.ShowMessage(Directory.GetCurrentDirectory());
        }
        private void OutputWriteLine(object obj,string text)
        {
            dialogService.AddText(obj, text);
        }

        private void RunConnectAsTcpClient(object obj)
        {
            Task _ = ConnectAsTcpClient(obj,"127.0.0.1", 1234);
        }

        private async Task ConnectAsTcpClient(object obj, string ip, int port)
        {
            int x = Array.IndexOf(vmObject.ViewModels.ToArray(), vmObject.SelectedViewModel);

            vmObject.ViewModels[x].CurrentState = IPageViewModel.States.Yellow;

            for (; ; )
            {
                try
                {
                    await Task.Delay(millisecondsDelay: 100);

                    long ellapledTicks = DateTime.Now.Second;

                    using (var tcpClient = new TcpClient())
                    {
                        Task connectTask = tcpClient.ConnectAsync(ip, port);
                        Task timeoutTask = Task.Delay(millisecondsDelay: 100);
                        if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                        {
                            throw new TimeoutException();
                        }

                        using (var networkStream = tcpClient.GetStream())
                        using (var reader = new StreamReader(networkStream))
                        using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
                        {

                            await writer.WriteLineAsync(ClientRequestString);
                            try
                            {
                                for (; ; )
                                {
                                    var response = await reader.ReadLineAsync();
                                    if (response == "")
                                    {
                                        ellapledTicks = DateTime.Now.Second - ellapledTicks;
                                        vmObject.ViewModels[x].CurrentState = IPageViewModel.States.Green;

                                        break;
                                    }
                                    if (response == null) 
                                    {
                                        break;
                                    }
                                    //TODO
                                    vmObject.ViewModels[x].DataRecordsList = dataService.updateDataRerordsList(vmObject.ViewModels[x].DataRecordsList, response + "\n");
                                    vmObject.ViewModels[x].CurrentRow = vmObject.ViewModels[x].RowCount;
                                    vmObject.ViewModels[x].RowCount = vmObject.ViewModels[x].DataRecordsList.Length - 1;
                                    //TODO
                                }

                            }
                            catch (IOException)
                            {

                            }
                        }
                      
                    }

                }
                catch (TimeoutException)
                {
                    // reconnect
                }
            }
        }

        private static readonly string ClientRequestString = "Hello Mr server";

        private void AddViewModel(object arg)
        {
            if (arg != null)
            {
                string[] words = arg.ToString().Split(new char[] { '.' });
                string inputString = words[words.Length - 1];

                switch (inputString)
                {
                    case "TcpServerViewModel":
                        AddNewViewModelAndRemoveSelectedViewModel(new TcpServerViewModel(syncContext, dialogService, dataService, this) { });
                        break;

                    case "_1553MTViewModel":
                        AddNewViewModelAndRemoveSelectedViewModel(new _1553MTViewModel(syncContext,dialogService,dataService, this) { });
                        break;

                    case "ChoosePageViewModel":
                        vmObject.ViewModels.Add(new ChoosePageViewModel(syncContext, dialogService, dataService, this) { });
                        vmObject.SelectedViewModel = vmObject.ViewModels[vmObject.ViewModels.Count - 1];
                        break;

                    default: 
                        dialogService.ShowMessage("Nothing");
                        break;
                }

            }

        }

        private void RemoveViewModel()
        {
            if (vmObject.ViewModels.Count > 2)
            {
                vmObject.ViewModels.Remove(vmObject.SelectedViewModel);
                var SelectedViewModelCount = vmObject.ViewModels.IndexOf(vmObject.SelectedViewModel);
                vmObject.SelectedViewModel = vmObject.ViewModels[SelectedViewModelCount];
            }
            else if (vmObject.ViewModels.Count > 1)
            {
                vmObject.ViewModels.Remove(vmObject.SelectedViewModel);
                vmObject.SelectedViewModel = vmObject.ViewModels[0];
            }
            else
            {
                vmObject.ViewModels.Remove(vmObject.SelectedViewModel);
            }   
        }

        private void AddNewViewModelAndRemoveSelectedViewModel(IPageViewModel newViewModel)
        {
            var SelectedViewModelCount = vmObject.ViewModels.IndexOf(vmObject.SelectedViewModel);

            vmObject.ViewModels.Remove(vmObject.SelectedViewModel);
            vmObject.ViewModels.Insert(SelectedViewModelCount, newViewModel);
           
            vmObject.SelectedViewModel = vmObject.ViewModels[SelectedViewModelCount];

            OpenFileForDataCreation();
        }

        private void UpdateCurrentRow(object arg)
        {
            ScrollAndChangeData(arg);
        }
        
        private void NextCurrentRow(object arg)
        {
            if (vmObject.SelectedViewModel.CurrentRow < vmObject.SelectedViewModel.RowCount)
            {
                vmObject.SelectedViewModel.CurrentRow++;
            }
        }

        private void PrevCurrentRow(object arg)
        {
            if (vmObject.SelectedViewModel.CurrentRow > 0)
            {
                vmObject.SelectedViewModel.CurrentRow--;
            }
        }

        private void ScrollAndChangeData(object arg)
        {
            if (vmObject.SelectedViewModel.CurrentRow < vmObject.SelectedViewModel.DataRecordsList.Length)
            {
                dialogService.ScrollIntoView(arg, vmObject.SelectedViewModel.CurrentRow);
            }
        }

        private void OpenFileForDataCreation()
        {


            //using (StreamReader reader = new StreamReader(dialogService.ShowOpenFileDialog()))
            using (StreamReader reader = new StreamReader(vmObject.SelectedViewModel.dialogService.ShowOpenFileDialog()))
            {
                string path = reader.ReadToEnd();
                if (File.Exists(path))
                {
                    SelectedDataUpdate(path);
                }
            }
        }

        private void OpenXmlForTableCreation(string path)
        {
            if (File.Exists(path))
            {
                SelectedDataUpdate(path);
            }
        }

        private void SelectedDataUpdate(string path)
        {
            vmObject.SelectedViewModel.CurrentState = IPageViewModel.States.Yellow;

            vmObject.SelectedViewModel.DataRecordsList = dataService.GetData(path, vmObject.SelectedViewModel.Name);
            vmObject.SelectedViewModel.CurrentRow = 0;
            vmObject.SelectedViewModel.RowCount = vmObject.SelectedViewModel.DataRecordsList.Length - 1;

            if (vmObject.SelectedViewModel.RowCount != -1)
            {
                vmObject.SelectedViewModel.CurrentState = IPageViewModel.States.Green;
            }
            else
            {
                vmObject.SelectedViewModel.CurrentState = IPageViewModel.States.Red;
            }
           
        }

        private void SaveXmlFromTable()
        {
            // convert stream to string
            StreamReader reader = new StreamReader(dialogService.ShowSaveFileDialog());
            string path = reader.ReadToEnd();
        }

        private void ShowHelpInformation(object obj)
        {
            if (obj!=null)
            {
                dialogService.ShowMessage(obj.ToString());
            }
            else
            {
                dialogService.CreateCustomWindow();
            }
        }

        private void DoNothing()
        {

        }

        #endregion


        ICommand CreateCommand(Func<Task> command) => ReactiveCommand.CreateFromTask(command, outputScheduler: scheduler);

        ICommand CreateCommand<T>(Func<T, Task> command) => ReactiveCommand.CreateFromTask(command, outputScheduler: scheduler);

        ICommand CreateCommand(Action command) => ReactiveCommand.Create(command, outputScheduler: scheduler);

        ICommand CreateCommand<T>(Action<T> command) => ReactiveCommand.Create(command, outputScheduler: scheduler);


    }
}
