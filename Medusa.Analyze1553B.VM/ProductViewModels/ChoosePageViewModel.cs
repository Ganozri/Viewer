using System;
using Medusa.Analyze1553B.UIServices;
using System.Collections.ObjectModel;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class ChoosePageViewModel : SupportClass, IPageViewModel
    {
        public ObservableCollection<string> ListViewModels { get; set; }

        public ChoosePageViewModel(ObservableCollection<Type> types,ISynchronizationContextProvider syncContext, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = GetType().Name;

            ListViewModels = new ObservableCollection<string>();
            foreach (var item in types)
                ListViewModels.Add(item.Name);
        }
    }
}
