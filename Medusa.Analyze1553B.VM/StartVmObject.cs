using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VMServices;
using Program.ByteSumCountingProgram.VM;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Program.ByteSumCountingProgram.VM.ViewModels;
using StructureMap.Graph.Scanning;
using System.Reflection;

namespace Program.ByteSumCountingProgram.VM
{
    public class StartVmObject : ReactiveObject, IVmObject
    {
        public string Title { get;set;}
        private readonly SynchronizationContext syncContext;
        public Commands Commands { get; }
        [Reactive] public object VmObject { get; private set; }

        [Reactive] public ObservableCollection<IPageViewModel> ViewModels { get; set; }
        [Reactive] public IPageViewModel SelectedViewModel { get; set; }

        public ObservableCollection<Type> Types { get; set; }

        public StartVmObject(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = new Commands(syncContext, this, dialogService, dataService);

            Title = "Viewer";

            Types = GetTypes();

            ViewModels = new ObservableCollection<IPageViewModel>();
            ViewModels.Add(new FirstViewModel(syncContext,dialogService,dataService,Commands));

            


        }
       
        private ObservableCollection<Type> GetTypes()
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

            return Types;
        }

        [Reactive] public double WindowScale { get; set; } = 1;
        
    }
}
