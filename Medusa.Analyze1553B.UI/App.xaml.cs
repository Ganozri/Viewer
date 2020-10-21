using System;
using System.IO;
using System.Windows;
using Program.ByteSumCountingProgram.DI;
using Program.ByteSumCountingProgram.UI;
using Program.ByteSumCountingProgram.UI.Views;
using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VM;
using Program.ByteSumCountingProgram.VMServices;

namespace Program.ByteSumCountingProgram.UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            TypeResolver.Container.Configure(
                r =>
                {
                    r.For<StartVmObject>().Transient();
                    r.For<MainWindow>().Singleton();
                    r.For<IDialogService>().Use(x => x.GetInstance<MainWindow>());
                    r.For<IDataService>().Use(x => x.GetInstance<DataService>());
                    r.For<ISynchronizationContextProvider>().Use(x => x.GetInstance<MainWindow>());
                  
                });
            
            var wnd = TypeResolver.Resolve<MainWindow>();
            wnd.DataContext = TypeResolver.Resolve<StartVmObject>();
            
            this.MainWindow = wnd;
            wnd.Show();

        }
    }
}
