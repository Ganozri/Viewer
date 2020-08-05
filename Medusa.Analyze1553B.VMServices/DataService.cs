using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UIServices;
using Olympus.Checkers;
using Olympus.Translation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Medusa.Analyze1553B.VMServices
{
    public class DataService : IDataService
    {
        private readonly IDialogService dialogService;

        private TranslationRepository translationRepository;
        private Medusa.Analyze1553B.Loader.BMD.Loader loader;

        private ObservableCollection<DataRecord> dataRecordsList_;

        public DataService(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            translationRepository = new TranslationRepository();
            loader = new Medusa.Analyze1553B.Loader.BMD.Loader(translationRepository);
        }

        public object[] Data(int currentRow, object[] currentData)
        {
                //
                IEnumerable<DataRecord> dr = currentData.Cast<DataRecord>();
                dataRecordsList_ = new ObservableCollection<DataRecord>(dr.ToList<DataRecord>());
                //
                ObservableCollection<ushort> Data_ = new ObservableCollection<ushort>(dataRecordsList_[currentRow].Data);
                object[] Data = Data_.Select(x => x as object).ToArray();

                return Data;       
        }

        public object[] dataRecordsList(string path)
        {
            FileStream fstream = File.OpenRead(path);
            IEnumerable<DataRecord> dataRecords = loader.ReadStream(fstream);
            dataRecordsList_ = new ObservableCollection<DataRecord>(dataRecords.ToList<DataRecord>());
            object[] dataRecordsList = dataRecordsList_.Select(x => x as object).ToArray();

            return dataRecordsList;
        }

        public object[] updateDataRerordsList(string input)
        {
            using (var stream = GenerateStreamFromString(input))
            {
                IEnumerable<DataRecord> dataRecords = loader.ReadStream(stream);
                dataRecordsList_ = new ObservableCollection<DataRecord>(dataRecords.ToList<DataRecord>());
                object[] dataRecordsList = dataRecordsList_.Select(x => x as object).ToArray();

                return dataRecordsList;
            }
            
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


    }
}
