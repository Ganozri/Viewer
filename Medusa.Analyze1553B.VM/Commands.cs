using System;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
using System.Xml;
using System.Text;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using ReactiveUI;
using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VMServices;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Program.ByteSumCountingProgram.VM;
using System.Net.Sockets;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Program.ByteSumCountingProgram.VM
{
    public partial class Commands
    {
        public readonly IScheduler scheduler;
        public readonly IVmObject vmObject;
        public readonly IDialogService dialogService;
        public readonly IDataService dataService;

        public readonly ISynchronizationContextProvider syncContext;

        #region Commands

        public ICommand DoNothingCommand { get; }
        public ICommand CreateManagerOfViewModelsCommand { get;}
        public ICommand AddViewModelCommand  { get; }

        public ICommand AddItemCommand { get; }
        public ICommand CloseSomethingCommand { get; }
        public ICommand GetDataFromFileCommand { get; }
        

        #endregion

        public Commands(ISynchronizationContextProvider syncContext, IVmObject vmObject, IDialogService dialogService, IDataService dataService)
        {
            this.vmObject       = vmObject;
            this.dialogService  = dialogService;
            this.dataService    = dataService;
            this.scheduler      = new SynchronizationContextScheduler(syncContext.SynchronizationContext);
            this.syncContext    = syncContext;

            DoNothingCommand                = CreateCommand<object>(DoNothing);
            CreateManagerOfViewModelsCommand = CreateCommand(CreateManagerOfViewModels);
            AddViewModelCommand = CreateCommand<IPageViewModel>(AddViewModel);
           
            AddItemCommand = CreateCommand<object>(AddItem);
            CloseSomethingCommand = CreateCommand<object>(CloseSomething);
            GetDataFromFileCommand = CreateCommand(GetDataFromFile);
        }

       

        ICommand CreateCommand(Func<Task> command) => ReactiveCommand.CreateFromTask(command, outputScheduler: scheduler);

        ICommand CreateCommand<T>(Func<T, Task> command) => ReactiveCommand.CreateFromTask(command, outputScheduler: scheduler);

        ICommand CreateCommand(Action command) => ReactiveCommand.Create(command, outputScheduler: scheduler);

        ICommand CreateCommand<T>(Action<T> command) => ReactiveCommand.Create(command, outputScheduler: scheduler);


    }
}
