using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class TcpServerViewModel : SupportClass, IPageViewModel
    {
        public TcpServerViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            Name = GetType().Name;
            DialogService = dialogService;
            DialogService.Filter = "BMD files(*.bmd) | *.bmd";
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;
            FillData();
        }
    }
}
