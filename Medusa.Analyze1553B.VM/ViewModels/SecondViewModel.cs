using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.Data;
using DynamicData.Binding;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class SecondViewModel : CommonViewModelClass, IPageViewModel
    {
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public Item SelectedItem { get; set; }
        public ObservableCollectionExtended<DataRecord> DataRecords { get;  }
        public string Name {get;set;}
        

        public SecondViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            DialogService = dialogService;
            DialogService.Filter = "TXT files (*.txt)|*.txt";
            this.SyncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "Вторая бизнес-модель";
            DataRecords = new ObservableCollectionExtended<DataRecord>();

            Nodes = new ObservableCollection<Node>
            {
                new Node
                {
                    Name = "Общее",
                    Nodes = new ObservableCollection<Node>
                    {
                        new Node
                        {
                            Name ="ССД"
                        },

                        new Node
                        {
                            Name ="Время"
                        },
                        new Node { Name="Время(полученное)" },
                        new Node { Name="Исправность устройства" },
                        new Node { Name="Управление терминалом" },
                        new Node
                        {
                            Name ="DDB",
                        },
                        new Node
                        {
                            Name ="DBT",
                            Nodes = new ObservableCollection<Node>
                            {
                                new Node {Name="Выгрузка данных сервиса управления памятью" },
                                new Node {Name="Пакеты команд и телеметрии" }
                            }
                        }
                    }
                }
            };

            Items = new ObservableCollection<Item>();

            Items = new ObservableCollection<Item>();
            Items.Add(new Item(DataRecords, Nodes[0]));

        } 
    } 
}

