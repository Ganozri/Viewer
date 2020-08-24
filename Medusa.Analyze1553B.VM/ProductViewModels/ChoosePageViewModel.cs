using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.IO;
using System.Collections.ObjectModel;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class ChoosePageViewModel : SupportClass, IPageViewModel
    {

        public ObservableCollection<IPageViewModel> ListViewModels { get; set; }

        public ChoosePageViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "ChoosePageViewModel";

            //
            ListViewModels = new ObservableCollection<IPageViewModel>();
            ListViewModels.Add(new MedusaViewModel(syncContext, dialogService, dataService, Commands) { });
            ListViewModels.Add(new TestViewModel(syncContext, dialogService, dataService, Commands) { });
            ListViewModels.Add(new TcpServerViewModel(syncContext, dialogService, dataService, Commands) { });
            ListViewModels.Add(new _1553MTViewModel(syncContext, dialogService, dataService, Commands) { });
            //
            //FillData(dataService);
        }
    }
}
