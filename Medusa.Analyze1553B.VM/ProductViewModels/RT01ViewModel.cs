using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class RT01ViewModel : SupportClass, IPageViewModel
    {
        public RT01ViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            Name = GetType().Name;
            DialogService = dialogService;
            DialogService.Filter = "CSV files (*.csv)|*.csv";
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;
            FillData();

        }
    }
}
