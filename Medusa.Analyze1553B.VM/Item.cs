using DynamicData;
using DynamicData.Binding;

using Medusa.Analyze1553B.VM.ViewModels;
using Model;
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

        public readonly ReadOnlyObservableCollection<BaseMainModel> _ReadableMainModels;
        public ReadOnlyObservableCollection<BaseMainModel> ReadableMainModels => _ReadableMainModels;
        [Reactive] public int SelectedMainModelIndex { get;set;}
        [Reactive] public BaseMainModel SelectedMainModel { get; set; }

        public Item(ObservableCollectionExtended<MainModel> Source,Node node)
        {
            this.Name = node.Name;
            this.type = node.type;
            //
            if (node.type!=null)
            {
                this.type = node.type;

                Source.ToObservableChangeSet()
                  .Filter(t => node.FiltrationCondition(t))
                  .Transform(dataRecord => СreateSuccessorOfBaseMainModels(dataRecord,node))
                  .Bind(out _ReadableMainModels)
                  .Subscribe();
            }
            else
            {
                this.type = typeof(MainModel);

                Source.ToObservableChangeSet()
               .Filter(t => node.FiltrationCondition(t))
               .Transform(dataRecord => Create(dataRecord))
               .Bind(out _ReadableMainModels)
               .Subscribe();
            }
        }
        static BaseMainModel Create(MainModel dataRecord)
        {
            BaseMainModel baseMainModels = new ReadableMainModel(dataRecord);
            
            return baseMainModels;
        }
        static BaseMainModel СreateSuccessorOfBaseMainModels(MainModel dataRecord,Node node)
        {
            Type[] parameters = new Type[] { typeof(MainModel) };
            object[] values = new object[] { dataRecord };
            object obj = CreateMyObject(node.type, parameters, values);

            return (BaseMainModel)obj;
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
