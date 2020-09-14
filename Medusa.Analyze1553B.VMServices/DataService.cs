using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UIServices;
using Newtonsoft.Json;
using Olympus.Checkers;
using Olympus.Translation;
using Parsers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Medusa.Analyze1553B.VMServices
{
    public class DataService : IDataService
    {
        private readonly IDialogService dialogService;

        private Medusa.Analyze1553B.Loader.BMD.Loader loader;

        private string Name { get; set; }

        public DataService(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            loader = new Medusa.Analyze1553B.Loader.BMD.Loader(new TranslationRepository());
        }

        public object[] GetData(string path,string name)
        {
            return name switch
            {
                "TcpServerViewModel" => GetDataByBMDLoader(path),
                "_1553MTViewModel" => GetDataByParser1553MT(path),
                _ => null,
            };
        }

        public object[] GetDataByParser1553MT(string path)
        {
            object[] DataRecordsList = new object[0];

            try
            {
                var dataRecords = Parser1553MT.GetDataByPath(path);
                DataRecordsList = dataRecords.Cast<object>().ToArray();
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.ToString());
            }
            finally
            {
            }

            return DataRecordsList;
        }

        public object[] GetDataByBMDLoader(string path)
        {
            FileStream fstream = File.OpenRead(path);

            var dataRecords = loader.ReadStream(fstream);
            object[] DataRecordsList = dataRecords.Cast<object>().ToArray();

            return DataRecordsList;
        }

        public object[] updateDataRerordsList(string input)
        {
            using (var stream = GenerateStreamFromString(input))
            {
                var dataRecords = loader.ReadStream(stream);
                object[] DataRecordsList = dataRecords.Cast<object>().ToArray();

                return DataRecordsList;
            }
            
        }

        public object[] updateDataRerordsList(object[] currentData, string input)
        {
            using (var stream = GenerateStreamFromString(input))
            {
                IEnumerable<DataRecord> dataRecords = loader.ReadStream(stream);
                object[] DataRecordsList = dataRecords.Cast<object>().ToArray();

                int oldLength = currentData.Length;
                int addedLength = DataRecordsList.Length;

                Array.Resize<object>(ref currentData, oldLength + addedLength);

                for (int i = oldLength; i < (oldLength + addedLength); i++)
                {
                    currentData[i] = DataRecordsList[i - oldLength];
                }

                return currentData;
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
