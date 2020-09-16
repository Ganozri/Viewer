﻿using System;
using System.Collections.ObjectModel;


namespace Medusa.Analyze1553B.VM
{
    public interface IVmObject
    {
        object VmObject { get; }

        public Commands Commands { get; }
        public ObservableCollection<Type> Types { get; set; }

        ObservableCollection<IPageViewModel> ViewModels { get; set; }
        IPageViewModel SelectedViewModel { get; set; }
    }
}
