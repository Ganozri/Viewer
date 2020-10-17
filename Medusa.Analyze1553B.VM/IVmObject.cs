using System;
using System.Collections.ObjectModel;


namespace Medusa.Analyze1553B.VM
{
    public interface IVmObject
    {
        object VmObject { get; }

        public Commands Commands { get; }

        public ObservableCollection<IPageViewModel> ViewModels { get; set; }
        public IPageViewModel SelectedViewModel { get; set; }

        public ObservableCollection<Type> Types { get; set; }

    }
}
