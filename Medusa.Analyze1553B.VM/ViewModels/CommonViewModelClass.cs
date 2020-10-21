
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VMServices;
using System.Data;
using System.Threading;
using ReactiveUI.Fody.Helpers;

namespace Program.ByteSumCountingProgram.VM.ViewModels
{
    public class CommonViewModelClass
    {
        public Commands Commands { get; set; }
        public SynchronizationContext SyncContext;
        [Reactive] public IDialogService DialogService { get; set; }
    }
}
