﻿using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.VM.ProductViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Medusa.Analyze1553B.VM
{
    public class StartVmObject : ReactiveObject, IVmObject
    {
        private readonly SynchronizationContext syncContext;
        public Commands Commands { get; }
        public object VmObject { get; private set; }

        public ObservableCollection<IPageViewModel> ListViewModels { get; set; }

        [Reactive] public ObservableCollection<IPageViewModel> ViewModels { get; set; }
        [Reactive] public IPageViewModel SelectedViewModel { get; set; }

        public StartVmObject(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService)
        {

            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = new Commands(syncContext, this, dialogService, dataService);
            //
            ListViewModels = new ObservableCollection<IPageViewModel>();
            ListViewModels.Add(new MedusaViewModel(syncContext, dialogService, dataService, Commands) { });
            ListViewModels.Add(new TestViewModel(syncContext, dialogService, dataService, Commands)   { });
            //
            ViewModels = new ObservableCollection<IPageViewModel>();
            ViewModels.Add(new MedusaViewModel(syncContext, dialogService, dataService, Commands)   { Name = "MedusaVM"});
            ViewModels.Add(new MedusaViewModel(syncContext, dialogService, dataService, Commands)   { Name = "MedusaVM1" });
            ViewModels.Add(new TestViewModel(syncContext, dialogService, dataService, Commands)     { Name = "TestVM1"});
            ViewModels.Add(new TcpServerViewModel(syncContext, dialogService, dataService, Commands){ });

            SelectedViewModel = ViewModels[ViewModels.Count-1];

        }

    }
}