using System;
using Medusa.Analyze1553B.UIServices;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using DynamicData;

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
                if (fs.Length==0)
                {
                    formatter.Serialize(fs, PreviouslySelectedVM);
                }
                else
                {
                    PreviouslySelectedVM = (ObservableCollection<TypePath>)formatter.Deserialize(fs);
                    RemoveDuplicates();
                    fs.SetLength(0);
                    formatter.Serialize(fs, PreviouslySelectedVM);
                }
            }

        }

        public ObservableCollection<TypePath> RemoveDuplicates()
        {
            int i = 0;
            while (i < PreviouslySelectedVM.Count)
            {
                PreviouslySelectedVM = FilterByNumber<TypePath>(PreviouslySelectedVM, i);
                i++;
            }
            return PreviouslySelectedVM;
        }

        public ObservableCollection<TypePath> FilterByNumber<T>(ObservableCollection<TypePath> inputColeection,int x)
        {
            TypePath item = inputColeection[x];
            var result = inputColeection.Where(w => w.Type.Equals(item.Type) && w.Path.Equals(item.Path));
            var end = Convert<TypePath>(result);
            var r = Convert<TypePath>(inputColeection.Except(end));
            r.Insert(x, item);
            return r;
        }

        public ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            return new ObservableCollection<T>(original.Cast<T>());
        }
    }
}
