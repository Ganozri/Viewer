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
    public class TcpServerViewModel : SupportClass, IPageViewModel
    {

        public Commands Commands { get; }

        private readonly SynchronizationContext syncContext;
        //
        public string TextRow { get; set; }
        //
        public TcpServerViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "TcpServerViewModel";

            FillData(dataService);
        }


    }
}
