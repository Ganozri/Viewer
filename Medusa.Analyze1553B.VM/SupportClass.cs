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

        [Reactive] public object[] dataRecordsList { get; set; }
        [Reactive] public object[] Data { get; set; }
        [Reactive] public int currentRow { get; set; }
        [Reactive] public int rowCount { get; set; }

        [Reactive] public States currentState { get; set; }
        //
        public Commands Commands { get; set; }
        public SynchronizationContext syncContext;
        public IDataService dataService;
        //

        public SupportClass()
        {
            currentState = States.Red;
        }

        public void FillData(IDataService dataService, string path = "@")
        {

            //path = @"D:\Data\20200314-173833 (norm).bmd";
            path = @"";
            bool isFileExists = File.Exists(path);
            if (isFileExists)
            {
                dataRecordsList = dataService.GetDataByBMDLoader(path);
                currentRow = 0;
                rowCount = dataRecordsList.Length - 1;
                //Data = dataService.Data(currentRow, dataRecordsList);
            }
            else
            {
                dataRecordsList = new object[] { };
                currentRow = 0;
                rowCount = 0;
            }

        }

       
    }
}
