
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.Data;
using DynamicData.Binding;
using Model;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class SecondViewModel : CommonViewModelClass, IPageViewModel
    {
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public Item SelectedItem { get; set; }
        public ObservableCollectionExtended<MainModel> MainModels { get;  }
        public string Name {get;set;}

        private bool MoreThenFive(MainModel dataRecord)
        {
            return dataRecord.FirstNumber > 5;
        }
        private bool LessThenFive(MainModel dataRecord)
        {
            return dataRecord.FirstNumber < 5;
        }
        private void CreateNodes()
        {
            Nodes = new ObservableCollection<Node>
            {
                new Node
                {
                    Name = "ОбщееДругое",
                    type = typeof(ReadableMainModel),
                    Nodes = new ObservableCollection<Node>
                    {
                        new Node
                        {
                            Name ="Больше пятиДругое",
                            type = typeof(FirstRecord),
                            FiltrationCondition = MoreThenFive,
                        },
                        new Node
                        {
                            Name ="Меньше пятиДругое",
                            FiltrationCondition = LessThenFive,
                            type = typeof(SecondRecord),
                        }
                    }
                }
            };
        }
        private void CreateItems()
        {
            Items = new ObservableCollection<Item>();
        }
        public SecondViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            DialogService = dialogService;
            
            this.SyncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "Вторая бизнес-модель";
            MainModels = new ObservableCollectionExtended<MainModel>();
            CreateNodes();
            CreateItems();

            if (Nodes.Count > 0)
            {
                Items.Add(new Item(MainModels, Nodes[0]));
            }

        } 
    } 
}

