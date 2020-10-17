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
    public class SynchronizedCollectionsViewModel : ReactiveObject
    {
        private readonly ReadOnlyObservableCollection<DataRecord> _derived;
        public ReadOnlyObservableCollection<DataRecord> Derived => _derived;

        public ObservableCollectionExtended<DataRecord> Source { get; }

        public SynchronizedCollectionsViewModel()
        {
            Source = new ObservableCollectionExtended<DataRecord>();

            // Use the ToObservableChangeSet operator to convert
            // the observable collection to IObservable<IChangeSet<T>>
            // which describes the changes. Then, use any DD operators
            // to transform the collection. 
            Source.ToObservableChangeSet()
                .Filter(t=>t.Cw1.Subaddress==6)
                //.Transform(value => !value)
                // No need to use the .ObserveOn() operator here, as
                // ObservableCollectionExtended is single-threaded.
                .Bind(out _derived)
                .Subscribe();

            // Update the source collection and the derived
            // collection will update as well.
            //Source.Add(true);
            //Source.RemoveAt(0);
            //Source.Add(false);
            //Source.Add(true);
            string path = @"D:\Data\20200314-173833 (NEnorm).bmd";
            var x = GetDataByBMDLoader(path);
            for (int i = 0; i < 10; i++)
            {
                Source.Add(x[i]);
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
