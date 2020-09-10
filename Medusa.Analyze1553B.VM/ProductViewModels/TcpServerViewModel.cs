﻿using Medusa.Analyze1553B.UIServices;
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
using Medusa.Analyze1553B.Common;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class TcpServerViewModel : SupportClass, IPageViewModel
    {
        


        public TcpServerViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            Name = "TcpServerViewModel";
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            this.DialogService = dialogService;
            this.DialogService.Filter = "BMD files(*.bmd) | *.bmd";
            FillData(this.dataService);
        }

        
    }
}
