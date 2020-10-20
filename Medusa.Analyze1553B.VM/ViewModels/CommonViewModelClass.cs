
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.Data;
using System.Threading;
using ReactiveUI.Fody.Helpers;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class CommonViewModelClass
    {
        public Commands Commands { get; set; }
        public SynchronizationContext SyncContext;
        [Reactive] public IDialogService DialogService { get; set; }
    }
}
