using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class TcpServerViewModel : ReactiveObject, IPageViewModel
    {

        public Commands Commands { get; }
        public string Name { get; set; }

        [Reactive] public object[] dataRecordsList { get; set; }
        [Reactive] public object[] Data { get; set; }
        [Reactive] public int currentRow { get; set; }
        [Reactive] public int rowCount { get; set; }

        private readonly SynchronizationContext syncContext;
        //
        public string TextRow { get; set; }
        //
        public TcpServerViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            //
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;
            //
            Name = "TcpServerViewModel";
            //
            string path = @"";
            if (File.Exists(path))
            {
                dataRecordsList = dataService.dataRecordsList(path);
                currentRow = 0;
                rowCount = dataRecordsList.Length - 1;
                Data = dataService.Data(currentRow, dataRecordsList);
            }
            else
            {
                dataRecordsList = new object[] { };
                currentRow = 0;
                rowCount = 0;
                Data = new object[] { };
            }

        }


    }
}
