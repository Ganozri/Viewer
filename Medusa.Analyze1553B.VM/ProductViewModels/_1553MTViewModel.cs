using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.Common;
using Parsers;
using System.IO;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class _1553MTViewModel : SupportClass, IPageViewModel
    {
        public _1553MTViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "_1553MTViewModel";

            //FillData(dataService);
            //string path = @"D:\Data\1553-MT(TEST DATA).txt";

            string path = @"D:\Data\1553-MT.txt";
            bool isFileExists = File.Exists(path);
            if (isFileExists)
            {
                dataRecordsList = dataService.newDataRecordsList(path);
                currentRow = 0;
                rowCount = dataRecordsList.Length - 1;
                //Data = dataService.Data(currentRow, dataRecordsList);
            }
       



        }
    }
}
