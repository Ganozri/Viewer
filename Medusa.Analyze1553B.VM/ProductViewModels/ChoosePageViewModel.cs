using System;
using Medusa.Analyze1553B.UIServices;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

namespace Medusa.Analyze1553B.VM.ProductViewModels
{

    public class TypePath
    {
        public string Type { get; set; }
        public string Path { get; set; }
        public DateTime Time { get; set; }
    }

    public class ChoosePageViewModel : SupportClass, IPageViewModel
    {
        public ObservableCollection<string> ListViewModels { get; set; }
        public ObservableCollection<TypePath> PreviouslySelectedVM { get; set; }

        public ChoosePageViewModel(ObservableCollection<Type> types,ISynchronizationContextProvider syncContext, Commands Commands)
        {
            this.syncContext = syncContext.SynchronizationContext;
            this.Commands = Commands;

            Name = GetType().Name;

            ListViewModels = new ObservableCollection<string>();
            foreach (var item in types)
                ListViewModels.Add(item.Name);

            string pathToSave = "PreviouslySelectedProducts.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<TypePath>));

            ////PreviouslySelectedVM = new ObservableCollection<TypePath>();
            //PreviouslySelectedVM = new ObservableCollection<TypePath>
            //{
            //    new TypePath { Type = "RT01ViewModel", Path = @"D:\Data\20200710\2020-07-10-RT01-softwareupdate.csv", Time = DateTime.Now },
            //    new TypePath { Type = "RT01ViewModel", Path = @"D:\Data\20200710\2020-07-08-RT01.csv", Time = DateTime.Now },
            //    new TypePath { Type = "RT01ViewModel", Path = @"D:\Data\20200710\2020-07-08-RT01-2.csv", Time = DateTime.Now }
            //};

           

            //////получаем поток, куда будем записывать сериализованный объект
            //using (FileStream fs = new FileStream(pathToSave, FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, PreviouslySelectedVM);
            //}

            using (FileStream fs = new FileStream(pathToSave, FileMode.OpenOrCreate))
            {
                PreviouslySelectedVM = (ObservableCollection<TypePath>)formatter.Deserialize(fs);
            }

        }
    }
}
