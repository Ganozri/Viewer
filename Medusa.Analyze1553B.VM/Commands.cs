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

namespace Medusa.Analyze1553B.VM
{
    public partial class Commands
    {
        public readonly IScheduler scheduler;
        public readonly IVmObject vmObject;
        public readonly IDialogService dialogService;
        public readonly IDataService dataService;
        //
        public readonly ISynchronizationContextProvider syncContext;
        //

        #region Commands
        public ICommand DoNothingCommand { get; }

        public ICommand NextCurrentRowCommand { get; }
        public ICommand PrevCurrentRowCommand { get; }

        public ICommand OpenXmlForTableCreationCommand { get; }
        public ICommand SaveXmlFromTableCommand { get; }
        public ICommand ShowHelpInformationCommand { get; }
        //
        public ICommand UpdateCurrentRowCommand { get; }
        public ICommand AddVMCommand { get; }

        public ICommand ConnectAsTcpClientCommand { get; }
        public ICommand TestCommand { get; }
        //
        #endregion


        public Commands(ISynchronizationContextProvider syncContext, IVmObject vmObject, IDialogService dialogService, IDataService dataService)
        {
            this.vmObject = vmObject;
            this.dialogService = dialogService;
            this.dataService = dataService;
            this.scheduler = new SynchronizationContextScheduler(syncContext.SynchronizationContext);
            this.syncContext = syncContext;

            DoNothingCommand = CreateCommand(DoNothing);

            NextCurrentRowCommand = CreateCommand<object>(NextCurrentRow);
            PrevCurrentRowCommand = CreateCommand<object>(PrevCurrentRow);
            OpenXmlForTableCreationCommand = CreateCommand(OpenXmlForTableCreation);
            SaveXmlFromTableCommand = CreateCommand(SaveXmlFromTable);
            ShowHelpInformationCommand = CreateCommand<object>(ShowHelpInformation);
            UpdateCurrentRowCommand = CreateCommand<object>(UpdateCurrentRow);
            AddVMCommand = CreateCommand<object>(AddVM);
            ConnectAsTcpClientCommand = CreateCommand<object>(RunConnectAsTcpClient);
            TestCommand = CreateCommand<object>(Test);

        }



        #region Command Implementation
        //
        private void Test(object obj)
        {
            long ellapledTicks = DateTime.Now.Second;

            vmObject.SelectedViewModel.dataRecordsList[1] = vmObject.SelectedViewModel.dataRecordsList[0];
            object[] o = vmObject.SelectedViewModel.dataRecordsList;
            Array.Resize<object>(ref o, o.Length + 1);
            Array.Resize<object>(ref o, o.Length - 1);
            vmObject.SelectedViewModel.dataRecordsList = o;

            //dialogService.UpdateView(obj);
            ellapledTicks = DateTime.Now.Second - ellapledTicks;
            dialogService.ShowMessage(ellapledTicks.ToString());
        }
        private void OutputWriteLine(object obj,string text)
        {
            dialogService.AddText(obj, text);
            //output.Text = output.Text + "\n" + text;
            //outputScroll.ScrollToEnd();
        }

        private void RunConnectAsTcpClient(object obj)
        {
            //
             //vmObject.SelectedViewModel.dataRecordsList = new object[] { };
             //vmObject.SelectedViewModel.currentRow = 0;
             //vmObject.SelectedViewModel.rowCount = 0;
             //vmObject.SelectedViewModel.Data = new object[] { };
            //
            Task _ = ConnectAsTcpClient(obj,"127.0.0.1", 1234);
        }

        private async Task ConnectAsTcpClient(object obj, string ip, int port)
        {
            int x = Array.IndexOf(vmObject.ViewModels.ToArray(), vmObject.SelectedViewModel);
            //
            int count = 0;
            long ellapledTicks = DateTime.Now.Second;
            //
            for (; ; )
            {
                try
                {
                    await Task.Delay(millisecondsDelay: 1000);
                    using (var tcpClient = new TcpClient())
                    {
                        //OutputWriteLine(obj,"[Client] Attempting connection to server " + ip + ":" + port);
                        Task connectTask = tcpClient.ConnectAsync(ip, port);
                        Task timeoutTask = Task.Delay(millisecondsDelay: 100);
                        if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                        {
                            throw new TimeoutException();
                        }

                        OutputWriteLine(obj,"[Client] Connected to server");
                        using (var networkStream = tcpClient.GetStream())
                        using (var reader = new StreamReader(networkStream))
                        using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
                        {
                            OutputWriteLine(obj, string.Format("[Client] Writing request '{0}'", ClientRequestString));
                            await writer.WriteLineAsync(ClientRequestString);
                            try
                            {
                                for (; ; )
                                {
                                    var response = await reader.ReadLineAsync();
                                    if (response == "")
                                    {
                                        ellapledTicks = DateTime.Now.Second - ellapledTicks;
                                        OutputWriteLine(obj, "ellapledTicks = " + ellapledTicks.ToString());
                                        break;
                                    }
                                    if (response == null) 
                                    {
                                        break;
                                    }
                                    //OutputWriteLine(obj,count.ToString());
                                    //OutputWriteLine(obj, response);
                                    count++;
                                    //TODO

                                    

                                    vmObject.ViewModels[x].dataRecordsList = dataService.updateDataRerordsList(vmObject.ViewModels[x].dataRecordsList, response + "\n");

                                    vmObject.ViewModels[x].currentRow = vmObject.ViewModels[x].rowCount;
                                    vmObject.ViewModels[x].rowCount = vmObject.ViewModels[x].dataRecordsList.Length - 1;
                                    //vmObject.SelectedViewModel.dataRecordsList = dataService.updateDataRerordsList(vmObject.SelectedViewModel.dataRecordsList, response + "\n");
                                    //vmObject.SelectedViewModel.currentRow = 0;
                                    //vmObject.SelectedViewModel.rowCount = vmObject.SelectedViewModel.dataRecordsList.Length - 1;
                                    //TODO
                                }
                                OutputWriteLine(obj,"[Client] Server disconnected");
                            }
                            catch (IOException)
                            {
                                OutputWriteLine(obj,"[Client] Server disconnected");
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

        private void AddVM(object arg)
        {
            int x = 666;
            if (Object.ReferenceEquals(arg.GetType(), x.GetType()))
            {
                x = Convert.ToInt32(arg);
                string s = vmObject.ListViewModels[x].GetType().ToString();
                //dialogService.ShowMessage(s);
                object obj = vmObject.ListViewModels[x];
                vmObject.ViewModels.Add((IPageViewModel)obj);
            }
        }

        private void UpdateCurrentRow(object arg)
        {
            ScrollAndChangeData(arg);
        }
        
        private void NextCurrentRow(object arg)
        {
            if (vmObject.SelectedViewModel.currentRow < vmObject.SelectedViewModel.rowCount)
            {
                vmObject.SelectedViewModel.currentRow++;
                ScrollAndChangeData(arg);
            }
        }

        private void PrevCurrentRow(object arg)
        {
            if (vmObject.SelectedViewModel.currentRow > 0)
            {
                vmObject.SelectedViewModel.currentRow--;
                ScrollAndChangeData(arg);
            }
        }

        private void ScrollAndChangeData(object arg)
        {
            if (vmObject.SelectedViewModel.currentRow < vmObject.SelectedViewModel.dataRecordsList.Length)
            {
                dialogService.ScrollIntoView(arg, vmObject.SelectedViewModel.currentRow);
                vmObject.SelectedViewModel.Data = dataService.Data(vmObject.SelectedViewModel.currentRow, vmObject.SelectedViewModel.dataRecordsList);
            }
            else
            { 
                dialogService.ShowMessage("vmObject.SelectedViewModel.currentRow = " + vmObject.SelectedViewModel.currentRow +
                                          "\nvmObject.SelectedViewModel.dataRecordsList.Length = " + vmObject.SelectedViewModel.Data.Length);
            }
        }

        private void OpenXmlForTableCreation()
        {
            StreamReader reader = new StreamReader(dialogService.ShowOpenFileDialog());
            string path = reader.ReadToEnd();
            if (File.Exists(path))
            {
                //TODO
                vmObject.SelectedViewModel.dataRecordsList = dataService.dataRecordsList(path);
                vmObject.SelectedViewModel.currentRow = 0;
                vmObject.SelectedViewModel.rowCount = vmObject.SelectedViewModel.dataRecordsList.Length - 1;
                vmObject.SelectedViewModel.Data = dataService.Data(vmObject.SelectedViewModel.currentRow, vmObject.SelectedViewModel.dataRecordsList);
                //TODO
            }

        }

        private void OpenXmlForTableCreation(string path)
        {
            if (File.Exists(path))
            {
                //TODO
                vmObject.SelectedViewModel.dataRecordsList = dataService.dataRecordsList(path);
                vmObject.SelectedViewModel.currentRow = 0;
                vmObject.SelectedViewModel.rowCount = vmObject.SelectedViewModel.dataRecordsList.Length - 1;
                vmObject.SelectedViewModel.Data = dataService.Data(vmObject.SelectedViewModel.currentRow, vmObject.SelectedViewModel.dataRecordsList);
                //TODO
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
