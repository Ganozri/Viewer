using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.Common;
using Parsers;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class _1553MTViewModel : SupportClass, IPageViewModel
    {
        public _1553MTViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "_1553MTViewModel";

            FillData<DataRecord1553MT>(dataService);
        }


    }
}
