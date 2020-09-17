using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.VM.ProductViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace Medusa.Analyze1553B.VM
{
    public class StartVmObject : ReactiveObject, IVmObject
    {
        private readonly SynchronizationContext syncContext;
        public Commands Commands { get; }
        public object VmObject { get; private set; }

        [Reactive] public ObservableCollection<IPageViewModel> ViewModels { get; set; }
        [Reactive] public IPageViewModel SelectedViewModel { get; set; }

        [Reactive] public ObservableCollection<Type> Types { get; set; }

        public StartVmObject(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService)
        {

            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = new Commands(syncContext, this, dialogService, dataService);

            GetTypes();

            ViewModels = new ObservableCollection<IPageViewModel>();
            ViewModels.Add(new ChoosePageViewModel(Types,syncContext, Commands) { });

            SelectedViewModel = ViewModels[ViewModels.Count - 1];
        }

        public void GetTypes()
        {
            var type = typeof(IPageViewModel);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => p.Name != "IPageViewModel" && p.Name != "ChoosePageViewModel")
                .ToList();

            Types = new ObservableCollection<Type>();
            foreach (var item in types)
                Types.Add(item); 
        }
 
        [Reactive] public double WindowScale { get; set; } = 1;
        
    }
}
