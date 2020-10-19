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

        public readonly ReadOnlyObservableCollection<BaseDataRecord> _ReadableDataRecords;
        public ReadOnlyObservableCollection<BaseDataRecord> ReadableDataRecords => _ReadableDataRecords;
        [Reactive] public int SelectedDataRecordIndex { get;set;}
        [Reactive] public BaseDataRecord SelectedDataRecord { get; set; }

        public Item(ObservableCollectionExtended<DataRecord> Source,Node node)
        {
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
        }
        static BaseDataRecord Create(DataRecord dataRecord)
        {
            BaseDataRecord baseDataRecords = new ReadableDataRecord(dataRecord);
            
            return baseDataRecords;
        }
        static BaseDataRecord СreateSuccessorOfBaseDataRecords(DataRecord dataRecord,Node node)
        {
            Type[] parameters = new Type[] { typeof(DataRecord) };
            object[] values = new object[] { dataRecord };
            object obj = CreateMyObject(node.type, parameters, values);

            return (BaseDataRecord)obj;
        }
            

        static object CreateMyObject(Type myType, Type[] parameters, object[] values)
        {
            // reflection (получаем конструктор по типам)
            ConstructorInfo info = myType.GetConstructor(parameters);
            // reflection (создаем объект, вызывая конструктор)
            object myObj = info.Invoke(values);

            return myObj;
        }

    }
}
