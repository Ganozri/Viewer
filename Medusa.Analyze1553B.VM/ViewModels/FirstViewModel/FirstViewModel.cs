using Medusa.Analyze1553B.Common;
using System.Collections.ObjectModel;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using ReactiveUI.Fody.Helpers;
using DynamicData.Binding;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class FirstViewModel : CommonViewModelClass, IPageViewModel
    {
        [Reactive] public string Name { get;set;}
        [Reactive] public ObservableCollection<Node> Nodes { get;set; }
        [Reactive] public ObservableCollection<Item> Items { get;set; }
        [Reactive] public Item SelectedItem { get; set; }
        [Reactive] public ObservableCollectionExtended<DataRecord> DataRecords { get;}
        
        private bool testSSD(DataRecord dataRecord)
        {
            return dataRecord.Cw1.Address == 31 
                && dataRecord.Cw1.Direction == DataDirection.R 
                && dataRecord.Cw1.Subaddress == 31 
                && dataRecord.Cw1.Length == 17;
        }
        private bool testTime(DataRecord dataRecord)
        {
            return dataRecord.Cw1.Address == 31
                && dataRecord.Cw1.Direction == DataDirection.R
                && dataRecord.Cw1.Subaddress == 29
                && dataRecord.Cw1.Length == 5;
        }
        private void CreateNodes()
        {
            Nodes = new ObservableCollection<Node>
            {
                new Node
                {
                    Name = "Общее",
                    type = typeof(ReadableDataRecord),
                    Nodes = new ObservableCollection<Node>
                    {
                        new Node
                        {
                            Name ="ССД",
                            type = typeof(ReadableDataRecord),
                            FiltrationCondition = testSSD,
                        },
                        new Node 
                        {
                            Name ="Время",
                            FiltrationCondition = testTime,
                            type = typeof(ReadableDataRecord),
                        },
                        new Node 
                        {
                            Name="Время(полученное)",
                            type = typeof(ReadableDataRecord),     
                        },
                        new Node 
                        {
                            Name="Исправность устройства",
                            type = typeof(ReadableDataRecord),
                        },
                        new Node 
                        {
                            Name="Управление терминалом",
                            type = typeof(ReadableDataRecord), 
                        },
                        new Node 
                        {
                            Name ="DDB",
                            type = typeof(ReadableDataRecord),                  
                        },
                        new Node
                        {
                            Name ="DBT",
                            type = typeof(ReadableDataRecord),
                            Nodes = new ObservableCollection<Node>
                            {
                                new Node 
                                {
                                    Name= "Выгрузка данных сервиса управления памятью",
                                    type = typeof(ReadableDataRecord)},
                                new Node 
                                {
                                    Name= "Пакеты команд и телеметрии",
                                    type = typeof(CommandAndTelemetryPackagesDataRecord)
                                }
                            }
                        }
                    }
                }
            };
        }
        private void CreateItems()
        {
            Items = new ObservableCollection<Item>();
        }

        public FirstViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            DialogService = dialogService;
            DialogService.Filter = "BMD files (*.bmd)|*.bmd";
           
            this.SyncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "Первая бизнес-модель";

            DataRecords = new ObservableCollectionExtended<DataRecord>();
            CreateNodes();
            CreateItems();

            //Items.Add(new Item(DataRecords, Nodes[0]));
            //Items.Add(new Item(DataRecords, Nodes[0].Nodes[1]));

            //var Rules = new List<Rule>
            //{
            //    new Rule(31, DataDirection.R, 31, 17, "ССД"                   ),
            //    new Rule(31, DataDirection.R, 29, 5,  "Время"                 ),
            //    new Rule(1,  DataDirection.T, 29, 10,  "Время(полученное)"    ),
            //    new Rule(1,  DataDirection.T, 1, 32,  "Исправность устройства"),
            //    new Rule(1,  DataDirection.R, 1, 32,  "Управление терминалом" ),
            //    new Rule(31, DataDirection.R, 7, 32,  "Навигация и эл. орбиты"),
            //    new Rule(31, DataDirection.R, 10, 19,  "Инф. о времени эксп-я"),
            //    new Rule(1,  DataDirection.T, 2, 32,  "Рег. ТМ-1"             ),
            //    new Rule(1,  DataDirection.T, 3, 32,  "Рег. ТМ-2"             ),
            //    new Rule(1,  DataDirection.T, 4, 32,  "Рег. ТМ-3"             ),
            //    new Rule(1,  DataDirection.T, 5, 32,  "Рег. ТМ-4"             ),
            //    new Rule(1,  DataDirection.T, 6, 32,  "Данные УФПО"           ),
            //    new Rule(1,  DataDirection.T, 7, 32,  "Ориентация опт. осей"  ),
            //    new Rule(1,  DataDirection.T, 9, 32,  "Ориентация ОЭЗА"        ),
            //    new Rule(1,  DataDirection.T, 10, 29,  "Ориентация ТК"        ),
            //    new Rule(1,  DataDirection.T, 8, 32,  "DBG TM"                ),
            //    new Rule(1,  DataDirection.R, 27, 2,  "DTD"                   ),
            //    new Rule(1,  DataDirection.T, 27, 2,  "DTC"                   ),
            //    new Rule(1,  DataDirection.R, 11, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 12, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 13, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 14, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 15, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 16, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 17, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 18, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 19, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 20, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 21, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 22, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 23, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.R, 24, 8,  "DDB"                   ),
            //    new Rule(1,  DataDirection.T, 28, 2,  "ATR"                   ),
            //    new Rule(1,  DataDirection.R, 28, 2,  "ATC"                   ),
            //    new Rule(1,  DataDirection.T, 11, 9,  "ADB"                   )
            //};


        }
    }
}
