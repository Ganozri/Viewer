using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace CSharpConsole
{
    class Program : ReactiveObject
    {
       
        static void Main(string[] args)
        {
          var vm = new ExampleViewModel();
            vm.Name = "FirstName";
            vm.Name = "SecondName";
            
            var vm1 = new ExampleViewModel();
        }
        
    }

    public class ExampleViewModel : ReactiveObject
    {

        // Атрибут ReactiveUI.Fody, занимается
        // аспектно-ориентированным внедрением
        // OnPropertyChanged в сеттер Name.
        [Reactive] public string Name { get; set; }

        public ExampleViewModel()
        {
            // Слушаем OnPropertyChanged("Name").
            this.WhenAnyValue(x => x.Name)
                // Работаем с IObservable<string>
                .Subscribe(Console.WriteLine);
        }
    }
}
