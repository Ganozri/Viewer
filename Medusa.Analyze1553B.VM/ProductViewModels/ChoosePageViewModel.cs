using System;
using Medusa.Analyze1553B.UIServices;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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

            using (FileStream fs = new FileStream(pathToSave, FileMode.OpenOrCreate))
            {
                var dirtyList = (ObservableCollection<TypePath>)formatter.Deserialize(fs);
                List<TypePath> distinct = dirtyList.Distinct().ToList();
                PreviouslySelectedVM = new ObservableCollection<TypePath>(distinct);
            }

        }
    }
}
