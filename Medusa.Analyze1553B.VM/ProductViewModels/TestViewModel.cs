using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.IO;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class TestViewModel : SupportClass, IPageViewModel
    {
        //public Commands Commands { get; }
        //private readonly SynchronizationContext syncContext;

        public TestViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;
            
            Name = "TestViewModel";

            FillData(dataService);
        }
    }
}
