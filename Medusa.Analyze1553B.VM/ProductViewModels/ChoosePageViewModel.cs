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
        public ObservableCollection<string> ListViewModels { get; set; }
        public IDialogService DialogService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ChoosePageViewModel(ISynchronizationContextProvider syncContext, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "ChoosePageViewModel";

            ListViewModels = new ObservableCollection<string>();
            ListViewModels.Add("MedusaViewModel");
            ListViewModels.Add("TestViewModel");
            ListViewModels.Add("TcpServerViewModel");
            ListViewModels.Add("_1553MTViewModel");


            //FillData(dataService);
        }

        public ChoosePageViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "ChoosePageViewModel";

            ListViewModels = new ObservableCollection<string>();
            ListViewModels.Add("MedusaViewModel"   );
            ListViewModels.Add("TestViewModel"     );
            ListViewModels.Add("TcpServerViewModel");
            ListViewModels.Add("_1553MTViewModel"  );
            

            //FillData(dataService);
        }
    }
}
