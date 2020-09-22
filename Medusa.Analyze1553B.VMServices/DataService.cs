using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UIServices;
using Olympus.Translation;
using Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Medusa.Analyze1553B.VMServices
{
    public class DataService : IDataService
    {
        private readonly IDialogService dialogService;
        public DataService(IDialogService dialogService)
        {
            this.dialogService = dialogService; 
        }


        public object[] GetData(string path,string name)
        {
            return name switch
            {
                "TcpServerViewModel" => GetDataByBMDLoader(path),
                "MT1553ViewModel" => GetDataByParser1553MT(path),
                "RT01ViewModel" => GetDataByParserRT01(path),
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
            object[] DataRecordsList = new object[0];
            try
            {
                var loader = new Medusa.Analyze1553B.Loader.BMD.Loader(new TranslationRepository());
                var dataRecords = loader.ReadStream(fstream);
                DataRecordsList = dataRecords.Cast<object>().ToArray();
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.ToString());
            }


            return DataRecordsList;
        }

        public object[] GetDataByParserRT01(string path)
        {
            object[] DataRecordsList = new object[0];
            try
            {
                var dataRecords = ParserRT01.GetDataByPath(path);
                DataRecordsList = dataRecords.Cast<DataRecord>().ToArray();
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
