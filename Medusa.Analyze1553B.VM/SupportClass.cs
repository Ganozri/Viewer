using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ReactiveUI;
using static Medusa.Analyze1553B.VM.IPageViewModel;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.Common;

namespace Medusa.Analyze1553B.VM
{
    public class SupportClass : ReactiveObject
    {
        [Reactive] public string Name { get; set; }

        [Reactive] public object[] DataRecordsList { get; set; }
        [Reactive] public object[] Data { get; set; }
        [Reactive] public int CurrentRow { get; set; }
        [Reactive] public int RowCount { get; set; }

        [Reactive] public int NumberOfTransitions { get; set; }
        [Reactive] public bool IsPlay { get; set; }

        [Reactive] public States CurrentState { get; set; }
        //
        public Commands Commands { get; set; }
        public SynchronizationContext syncContext;
        public IDataService dataService;
        public IDialogService DialogService { get; set; }
        //

        public SupportClass()
        {
            CurrentState = States.Red;
            NumberOfTransitions = 1;
            IsPlay = false;
        }

        public void FillData(IDataService dataService, string path = "@")
        {
            path = @"";
            bool isFileExists = File.Exists(path);
            if (isFileExists)
            {
                DataRecordsList = dataService.GetDataByBMDLoader(path);
                CurrentRow = 0;
                RowCount = DataRecordsList.Length - 1;
            }
            else
            {
                DataRecordsList = new object[] { };
                CurrentRow = 0;
                RowCount = 0;
            }

        }

       
    }
}
