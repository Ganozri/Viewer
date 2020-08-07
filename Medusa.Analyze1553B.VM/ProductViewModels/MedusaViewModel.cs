using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.IO;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{
    public class MedusaViewModel : SupportClass, IPageViewModel
    {

        public Commands Commands { get; }

        private readonly SynchronizationContext syncContext;
        public MedusaViewModel(ISynchronizationContextProvider syncContext, IDialogService dialogService, IDataService dataService, Commands Commands)
        {
            //
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = "MedusaViewModel";
            string path = @"";
            if (File.Exists(path))
            {
                dataRecordsList = dataService.dataRecordsList(path);
                currentRow = 0;
                rowCount = dataRecordsList.Length - 1;
                Data = dataService.Data(currentRow, dataRecordsList);
            }
            else
            {
                dataRecordsList = new object[] { };
                currentRow = 0;
                rowCount = 0;
                Data = new object[] { };
            }

        }

       
    }
}
