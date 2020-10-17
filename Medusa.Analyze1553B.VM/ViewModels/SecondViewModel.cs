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

            
            DataTable Table = new DataTable();

            Table.Columns.Add("Type", typeof(string));
            Table.Columns.Add("MonitorTime", typeof(string));
            Table.Columns.Add("Cl", typeof(string));
            Table.Columns.Add("Error", typeof(string));
            Table.Columns.Add("Cw1", typeof(string));
            Table.Columns.Add("Addr", typeof(string));
            Table.Columns.Add("Dir", typeof(string));
            Table.Columns.Add("Sub", typeof(string));
            Table.Columns.Add("Length", typeof(string));
            Table.Columns.Add("Cw2", typeof(string));
            Table.Columns.Add("Rw1", typeof(string));
            Table.Columns.Add("Rw2", typeof(string));

            Table.Columns.Add("0", typeof(string));
            Table.Columns.Add("1", typeof(string));
            Table.Columns.Add("2", typeof(string));
            Table.Columns.Add("3", typeof(string));
            Table.Columns.Add("4", typeof(string));
            Table.Columns.Add("5", typeof(string));
            Table.Columns.Add("6", typeof(string));
            Table.Columns.Add("7", typeof(string));
            Table.Columns.Add("8", typeof(string));
            Table.Columns.Add("9", typeof(string));
            Table.Columns.Add("10", typeof(string));

            Table.Columns.Add("11", typeof(string));
            Table.Columns.Add("12", typeof(string));
            Table.Columns.Add("13", typeof(string));
            Table.Columns.Add("14", typeof(string));
            Table.Columns.Add("15", typeof(string));
            Table.Columns.Add("16", typeof(string));
            Table.Columns.Add("17", typeof(string));
            Table.Columns.Add("18", typeof(string));
            Table.Columns.Add("19", typeof(string));
            Table.Columns.Add("20", typeof(string));

            Table.Columns.Add("21", typeof(string));
            Table.Columns.Add("22", typeof(string));
            Table.Columns.Add("23", typeof(string));
            Table.Columns.Add("24", typeof(string));
            Table.Columns.Add("25", typeof(string));
            Table.Columns.Add("26", typeof(string));
            Table.Columns.Add("27", typeof(string));
            Table.Columns.Add("28", typeof(string));
            Table.Columns.Add("29", typeof(string));
            Table.Columns.Add("30", typeof(string));

            Table.Columns.Add("31", typeof(string));

            Items = new ObservableCollection<Item>();
            Items.Add(new Item(DataRecords, Nodes[0]));




            var Rules = new List<Rule>
            {
                new Rule(31, DataDirection.R, 31, 17, "ССД"                   ),
                new Rule(31, DataDirection.R, 29, 5,  "Время"                 ),
                new Rule(1,  DataDirection.T, 29, 10,  "Время(полученное)"    ),
                new Rule(1,  DataDirection.T, 1, 32,  "Исправность устройства"),
                new Rule(1,  DataDirection.R, 1, 32,  "Управление терминалом" ),
                new Rule(31, DataDirection.R, 7, 32,  "Навигация и эл. орбиты"),
                new Rule(31, DataDirection.R, 10, 19,  "Инф. о времени эксп-я"),
                new Rule(1,  DataDirection.T, 2, 32,  "Рег. ТМ-1"             ),
                new Rule(1,  DataDirection.T, 3, 32,  "Рег. ТМ-2"             ),
                new Rule(1,  DataDirection.T, 4, 32,  "Рег. ТМ-3"             ),
                new Rule(1,  DataDirection.T, 5, 32,  "Рег. ТМ-4"             ),
                new Rule(1,  DataDirection.T, 6, 32,  "Данные УФПО"           ),
                new Rule(1,  DataDirection.T, 7, 32,  "Ориентация опт. осей"  ),
                new Rule(1,  DataDirection.T, 9, 32,  "Ориентация ОЭЗА"        ),
                new Rule(1,  DataDirection.T, 10, 29,  "Ориентация ТК"        ),
                new Rule(1,  DataDirection.T, 8, 32,  "DBG TM"                ),
                new Rule(1,  DataDirection.R, 27, 2,  "DTD"                   ),
                new Rule(1,  DataDirection.T, 27, 2,  "DTC"                   ),
                new Rule(1,  DataDirection.R, 11, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 12, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 13, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 14, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 15, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 16, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 17, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 18, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 19, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 20, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 21, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 22, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 23, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.R, 24, 8,  "DDB"                   ),
                new Rule(1,  DataDirection.T, 28, 2,  "ATR"                   ),
                new Rule(1,  DataDirection.R, 28, 2,  "ATC"                   ),
                new Rule(1,  DataDirection.T, 11, 9,  "ADB"                   )
            };

            int count = 0;
            if (DataRecords!=null)
            {
                foreach (var record in DataRecords)
                {

                    Table.Rows.Add(
                        "",
                        record.MonitorTime,
                        record.Channel,
                        record.Error,
                        String.Format("{0:X4}", record.Cw1.Value),
                        record.Cw1.Address,
                        record.Cw1.Direction,
                        record.Cw1.Subaddress,
                        record.Cw1.Length,

                        record.Cw2.Value.Value,
                        record.Rw1.Value.Value,
                        record.Rw2.Value.Value);

                    foreach (var rule in Rules)
                    {
                        if (rule.Address == record.Cw1.Address
                            && rule.Direction == record.Cw1.Direction
                            && rule.Subaddress == record.Cw1.Subaddress
                            && rule.Length == record.Cw1.Length)
                        {
                            Table.Rows[count][0] = rule.Name;
                        }
                    }

                    for (int i = 12; i < 12 + record.Data.Length; i++)
                    {
                        Table.Rows[count][i] = String.Format("{0:X4}", record.Data[i - 12]);


                    }
                    count++;
                }
            }
           
        } 
    } 
}

