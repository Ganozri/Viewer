using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.Common;
using Parsers;
using System.IO;
using ReactiveUI.Fody.Helpers;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class MT1553ViewModel : SupportClass, IPageViewModel
    {
        public MT1553ViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            Name = "MT1553ViewModel";
            this.DialogService = dialogService;
            this.DialogService.Filter = "TXT files (*.txt)|*.txt";
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            FillData(this.dataService);
        }
    }
}
