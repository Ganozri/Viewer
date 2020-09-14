using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.Common;
using Parsers;
using System.IO;
using ReactiveUI.Fody.Helpers;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class RT01ViewModel : SupportClass, IPageViewModel
    {
        public RT01ViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            Name = "RT01ViewModel";
            this.DialogService = dialogService;
            this.DialogService.Filter = "CSV files (*.csv)|*.csv";
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            FillData(this.dataService);
        }
    }
}
