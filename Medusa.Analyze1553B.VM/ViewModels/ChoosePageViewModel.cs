
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VMServices;
using System.Data;
using System.Threading;
using ReactiveUI.Fody.Helpers;
using System.Reflection;
using DynamicData.Binding;
using Model;

namespace Program.ByteSumCountingProgram.VM.ViewModels
{
    public class ChoosePageViewModel : IPageViewModel
    {
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public Item SelectedItem { get; set; }
        public ObservableCollectionExtended<MainModel> MainModels { get; set; }
        [Reactive] public string Name { get; set; }

        public ObservableCollection<Type> Types { get; set; }
        public ObservableCollection<IPageViewModel> ViewModels { get; set; }

        public Commands Commands { get; set; }
        public SynchronizationContext SyncContext;
        public IDialogService DialogService { get; set; }
        public ChoosePageViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands commands)
        {
            Name = "";

            DialogService = dialogService;
            SyncContext   = syncContext.SynchronizationContext;
            this.Commands = commands;

            Types = Commands.vmObject.Types;

            ViewModels = new ObservableCollection<IPageViewModel>();

            foreach (var type in Types)
            {
                Type[] parameters = new Type[] { typeof(ISynchronizationContextProvider), typeof(IDialogService), typeof(IDataService), typeof(Commands) };
                object[] values = new object[] { syncContext, dialogService, dataService, Commands };
                object obj = CreateMyObject(type, parameters, values);

                ViewModels.Add((IPageViewModel)obj);
              
            }
            
            dialogService.CreateChoosePageViewModelControl(this);
        }

        static object CreateMyObject(Type myType, Type[] parameters, object[] values)
        {
            // reflection (получаем конструктор по типам)
            ConstructorInfo info = myType.GetConstructor(parameters);
            // reflection (создаем объект, вызывая конструктор)
            object myObj = info.Invoke(values);

            return myObj;
        }
    }
}

