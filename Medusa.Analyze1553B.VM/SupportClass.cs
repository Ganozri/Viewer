using System.Threading;
using ReactiveUI;
using static Medusa.Analyze1553B.VM.IPageViewModel;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.Common;

namespace Medusa.Analyze1553B.VM
{
    public class SupportClass : ReactiveObject
    {
        [Reactive] public string Name { get; set; }

        [Reactive] public DataRecord[] DataRecordsList { get; set; }

        [Reactive] public int CurrentRow { get; set; }
        [Reactive] public int RowCount { get; set; }

        [Reactive] public int NumberOfTransitions { get; set; }
        [Reactive] public bool IsPlay { get; set; }

        [Reactive] public States CurrentState { get; set; }
        //
        public Commands Commands { get; set; }
        public SynchronizationContext syncContext;
        public IDataService DataService;
        public IDialogService DialogService { get; set; }

        public SupportClass()
        {
            CurrentState = States.Red;
            NumberOfTransitions = 1;
            IsPlay = false;
        }

        public void FillData()
        {
            DataRecordsList = new DataRecord[] { };
            CurrentRow = 0;
            RowCount = 0;

            DialogService.Filter += "|All files (*.*)|*.*";
        }

       
    }
}
