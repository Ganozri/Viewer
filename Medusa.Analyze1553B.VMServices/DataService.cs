using Program.ByteSumCountingProgram.UIServices;
using Model;
using System;
using System.IO;
using System.Linq;


namespace Program.ByteSumCountingProgram.VMServices
{
    public class DataService : IDataService
    {
        private readonly IDialogService dialogService;
        public DataService(IDialogService dialogService)
        {
            this.dialogService = dialogService; 
        }
        
        public MainModel[] GetData(string path,object viewModel)
        {
            var words = viewModel.ToString().Split('.');
            var name = words[words.Length - 1];

            return name switch
            {
                "FirstViewModel" => GetDataByFirstParser(path),
                "SecondViewModel" => GetDataBySecondParser(path),
                _ => null,
            };
        }
        static MainModel[] GetRandomMainModelsForTest()
        {
            MainModel[] mainModels = new MainModel[100];
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                mainModels[i] = new MainModel();
            }

            return mainModels;
        }
        public MainModel[] GetDataByFirstParser(string path)
        {
            return BaseGetData(GetRandomMainModelsForTest());
        }
        //public MainModel[] GetDataByParser1553MT(string path)
        //{
        //    return BaseGetData(FirstParser.GetDataByPath(path));
        //}
        public MainModel[] GetDataBySecondParser(string path)
        {
            return BaseGetData(GetRandomMainModelsForTest());
        }
        //public MainModel[] GetDataByParser1553MT(string path)
        //{
        //    return BaseGetData(SecondParser.GetDataByPath(path));
        //}
        public object GetData(string path)
        {
            return this;
        }

        public MainModel[] BaseGetData(MainModel[] rawData)
        {
            MainModel[] MainModels = new MainModel[0];
            try
            {
                MainModels = rawData;
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.ToString());
            }
            return MainModels;
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
