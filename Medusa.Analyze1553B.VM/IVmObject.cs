using SqlStreamStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public interface IVmObject
    {
        object VmObject { get; }

        public Commands Commands { get; }
        ObservableCollection<IPageViewModel> ListViewModels { get; set; }
        ObservableCollection<IPageViewModel> ViewModels { get; set; }
        IPageViewModel SelectedViewModel { get; set; }
    }
}
