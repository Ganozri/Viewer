using DynamicData;
using DynamicData.Binding;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.Loader.BMD;
using Olympus.Translation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace CSharpWPF
{
    public class ViewModel : ReactiveObject
    {
        private readonly ReadOnlyObservableCollection<DataRecord> _items;
        public ReadOnlyObservableCollection<DataRecord> Items => _items;

        //public ObservableCollection<DataRecord> Source { get; }
        public SourceCache<DataRecord,int> Source { get; }
        public ViewModel()
        {

            //Source = new ObservableCollection<DataRecord>();
            var Source = new SourceCache<DataRecord, int>(t => (int)t.Index);
            Source
            //.ToObservableChangeSet(t => t.Index)
            .Connect()
            .Filter(t => t.Cw1.Subaddress==6)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();

            string path = @"D:\Data\20200314-173833 (NEnorm).bmd";
            var x = GetDataByBMDLoader(path);
            for (int i = 0; i < 10; i++)
            {
                Source.AddOrUpdate(x[i]);
            }
            //Source.AddRange(x);
        }
        public void AddRecord()
        {
            string path = @"D:\Data\20200314-173833 (NEnorm).bmd";
            var x = GetDataByBMDLoader(path);
            //Source.Add(x[4]);
        }
        public DataRecord[] GetDataByBMDLoader(string path)
        {
            return BaseGetData(new Loader(new TranslationRepository())
                               .ReadStream(File.OpenRead(path))
                               .ToArray());
        }

        public DataRecord[] BaseGetData(DataRecord[] rawData)
        {
            DataRecord[] DataRecords = new DataRecord[0];
            try
            {
                DataRecords = rawData;
            }
            catch (Exception ex)
            {
             
            }
            return DataRecords;
        }

    }
}
