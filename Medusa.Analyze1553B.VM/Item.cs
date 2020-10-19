using DynamicData;
using DynamicData.Binding;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM.ViewModels;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public sealed class Item
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Type type { get;set;}

        //public readonly ReadOnlyObservableCollection<DataRecord> _DataRecords;
        //public ReadOnlyObservableCollection<DataRecord> DataRecords => _DataRecords;
        //
        public readonly ReadOnlyObservableCollection<BaseDataRecords> _ReadableDataRecords;
        public ReadOnlyObservableCollection<BaseDataRecords> ReadableDataRecords => _ReadableDataRecords;
        [Reactive] public int SelectedDataRecordIndex { get;set;}
        [Reactive] public BaseDataRecords SelectedDataRecord { get; set; }
        //
        public Item(ObservableCollectionExtended<DataRecord> Source,Node node)
        {
            //Source.ToObservableChangeSet()
            //   .Filter(t => node.FiltrationCondition(t))
            //   //.Transform(value => !value)
            //   // No need to use the .ObserveOn() operator here, as
            //   // ObservableCollectionExtended is single-threaded.
            //   .Bind(out _DataRecords)
            //   .Subscribe();

            this.Name = node.Name;
            this.type = node.type;
            //
            if (node.type!=null)
            {
                this.type = node.type;

                Source.ToObservableChangeSet()
                  .Filter(t => node.FiltrationCondition(t))
                  .Transform(dataRecord => СreateSuccessorOfBaseDataRecords(dataRecord,node))
                  .Bind(out _ReadableDataRecords)
                  .Subscribe();

                //
                
                //
            }
            else
            {
                this.type = typeof(DataRecord);

                Source.ToObservableChangeSet()
               .Filter(t => node.FiltrationCondition(t))
               .Transform(dataRecord => Create(dataRecord))
               .Bind(out _ReadableDataRecords)
               .Subscribe();
            }
            //

        }
        static BaseDataRecords Create(DataRecord dataRecord)
        {
            BaseDataRecords baseDataRecords = new ReadableDataRecord(dataRecord);
            
            return baseDataRecords;
        }
        static BaseDataRecords СreateSuccessorOfBaseDataRecords(DataRecord dataRecord,Node node)
        {
            Type[] parameters = new Type[] { typeof(DataRecord) };
            object[] values = new object[] { dataRecord };
            object obj = CreateMyObject(node.type, parameters, values);

            return (BaseDataRecords)obj;
        }
            

        static object CreateMyObject(Type myType, Type[] parameters, object[] values)
        {
            // reflection (получаем конструктор по типам)
            ConstructorInfo info = myType.GetConstructor(parameters);
            // reflection (создаем объект, вызывая конструктор)
            object myObj = info.Invoke(values);

            return myObj;
        }
        //
    }
}
