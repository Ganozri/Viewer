using Medusa.Analyze1553B.UIServices;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public interface IPageViewModel 
    {
        string Name { get; set; }

        object[] DataRecordsList { get; set; }
        int CurrentRow { get; set; }
        int RowCount { get; set; }

        int NumberOfTransitions { get; set; }
        bool IsPlay { get; set; }
        States CurrentState { get; set; }

        IDialogService DialogService { get; set; }

        enum States
        {
            Red,
            Yellow,
            Green
        }

    }
}

