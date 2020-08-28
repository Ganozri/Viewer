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
        public string Name { get; set; }

        [Reactive] public object[] DataRecordsList { get; set; }
        [Reactive] public object[] Data { get; set; }
        [Reactive] public int CurrentRow { get; set; }
        [Reactive] public int RowCount { get; set; }

        [Reactive] public States CurrentState { get; set; }
        //
        public Commands Commands { get; set; }
        public SynchronizationContext syncContext;
        public IDataService dataService;
        //

        public SupportClass()
        {
            CurrentState = States.Red;
        }

        public void FillData(IDataService dataService, string path = "@")
        {

            //path = @"D:\Data\20200314-173833 (norm).bmd";
            path = @"";
            bool isFileExists = File.Exists(path);
            if (isFileExists)
            {
                DataRecordsList = dataService.GetDataByBMDLoader(path);
                CurrentRow = 0;
                RowCount = DataRecordsList.Length - 1;
                //Data = dataService.Data(CurrentRow, DataRecordsList);
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
