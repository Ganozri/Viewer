using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Peleng.Medusa.Analyze1553B
{
    class MainWindowViewModel : ReactiveObject, IMainWindowViewModel
    {
        [Reactive]
        public string Title { get; set; }

        public MainWindowViewModel()
        {
            System.Threading.Tasks.Task.Run(
                async () =>
                {
                    for (int i = 0; i < 100; ++i)
                    {
                        Title = "Hello world " + i;
                        await Task.Delay(100);
                    }
                }
            );
        }
    }
}
