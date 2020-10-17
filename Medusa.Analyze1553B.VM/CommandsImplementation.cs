using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medusa.Analyze1553B.VM
{
    public partial class Commands
    {
       
        private void GetDataFromFile()
        {
            if (vmObject.ViewModels.Count > 0 && vmObject.SelectedViewModel != null)
            {
                using (StreamReader reader = new StreamReader(vmObject.SelectedViewModel.DialogService.ShowOpenFileDialog()))
                {
                    string path = reader.ReadToEnd();
                    if (File.Exists(path))
                    {
                        vmObject.SelectedViewModel.DataRecords.Clear();

                        var rawData = dataService.GetData(path, vmObject.SelectedViewModel);

                        //vmObject.SelectedViewModel.DataRecords.AddRange(rawData);
                        _ = FactorialAsync(vmObject,rawData);
                    }
                }
            }
            else
            {
                dialogService.ShowMessage("Не выбран тип продукта!");
            }
           
        }

        //TODO
        // определение асинхронного метода
        static async Task FactorialAsync(IVmObject vmObject,DataRecord[] rawData)
        {
            if (rawData.Length > 1000)
            {
                int offset = 20;
                int count = 15;
                for (int i = 0; i < count; i++)
                {
                    ArraySegment<DataRecord> firstRecords = new ArraySegment<DataRecord>(rawData, i * offset, offset);
                    vmObject.SelectedViewModel.DataRecords.AddRange(firstRecords);
                    await Task.Delay(1);
                }
                ArraySegment<DataRecord> lastRecords = new ArraySegment<DataRecord>(rawData, offset*count, rawData.Length-offset*count);
                vmObject.SelectedViewModel.DataRecords.AddRange(lastRecords);
            }
            else
            {
                vmObject.SelectedViewModel.DataRecords.AddRange(rawData);
            }
        }
        //TODO
        private void AddViewModel(IPageViewModel pageViewModel)
        {
            vmObject.ViewModels.Add(pageViewModel);
        }
        private void CloseSomething(object obj)
        {
            switch (obj)
            {
                case IPageViewModel t1: vmObject.ViewModels.Remove((IPageViewModel)obj);break;
                case Item t2:vmObject.SelectedViewModel.Items.Remove((Item)obj);break;
                default: break;
            }
        }
        private void CreateManagerOfViewModels()
        {
            new ChoosePageViewModel(syncContext,dialogService,dataService,this);
        }
        private void AddItem(object obj)
        {
            if (obj != null)
            {
                if (obj is Node node)
                {
                    bool IsRepeat = false;
                    foreach (var item in vmObject.SelectedViewModel.Items)
                    {
                        if (item.Name == node.Name)
                        {
                            IsRepeat = true;
                            break;
                        }
                    }
                    if (!IsRepeat)
                    {
                        vmObject.SelectedViewModel.Items.Add(new Item(vmObject.SelectedViewModel.DataRecords,node));
                    }
                }


            }
        }
        private void DoNothing(object obj)
        {
            //dialogService.ShowMessage(obj.ToString());
            
        }
    }
}
