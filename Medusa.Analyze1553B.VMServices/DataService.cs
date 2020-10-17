using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UIServices;
using Olympus.Translation;
using Parsers;
using System;
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

        public DataRecord[] GetData(string path,object viewModel)
        {
            var words = viewModel.ToString().Split('.');
            var name = words[words.Length - 1];

            return name switch
            {
                "FirstViewModel" => GetDataByBMDLoader(path),
                "SecondViewModel" => GetDataByParser1553MT(path),
                _ => null,
            };
        }

        public object GetData(string path)
        {
            return this;
        }

        public DataRecord[] GetDataByParser1553MT(string path)
        {
            return BaseGetData(Parser1553MT.GetDataByPath(path));
        }

        public DataRecord[] GetDataByParserRT01(string path)
        {
            return BaseGetData(ParserRT01.GetDataByPath(path));
        }

        public DataRecord[] GetDataByBMDLoader(string path)
        {
            return BaseGetData(new Loader.BMD.Loader(new TranslationRepository())
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
                dialogService.ShowMessage(ex.ToString());
            }
            return DataRecords;
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
