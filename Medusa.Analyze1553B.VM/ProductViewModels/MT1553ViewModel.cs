using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class MT1553ViewModel : SupportClass, IPageViewModel
    {
        public MT1553ViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {;
            Name = GetType().Name;
            DialogService = dialogService;
            DialogService.Filter = "TXT files (*.txt)|*.txt";
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;
            FillData();
        }
    }
}
