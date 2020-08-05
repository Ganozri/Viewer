using System;
using System.IO;
using System.Windows;
using Medusa.Analyze1553B.DI;
using Medusa.Analyze1553B.UI.Views;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VM;
using Medusa.Analyze1553B.VMServices;
//using Microsoft.Extensions.DependencyInjection;
using Olympus.Translation;

namespace Medusa.Analyze1553B.UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        //private readonly IServiceProvider serviceProvider;

        //public App()
        //{
        //    var serviceCollection = new ServiceCollection();
        //    ConfigureServices(serviceCollection);

        //    serviceProvider = serviceCollection.BuildServiceProvider();
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    serviceProvider.GetService<MainWindow>().Show();
        //}

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddTransient<MainWindow>();
        //    services.AddTransient<IMainWindowViewModel, MainWindowViewModel>();
        //    //services.AddTransient<Medusa.Analyze1553B.UIServices.IDialogService>();
        //    services.AddSingleton(_ => LoadTranslation());
        //}

        //private TranslationRepository LoadTranslation()
        //{
        //    TranslationRepository repo = new TranslationRepository();
        //    var directory = System.IO.Path.GetDirectoryName(typeof(App).Assembly.Location) ?? Environment.CurrentDirectory;
        //    var files = Directory.GetFiles(directory, "*.xlg", SearchOption.AllDirectories);
        //    ForEach(files, x => repo.AddTranslationFile(x));

        //    repo.AllowTrace(true);
        //    this.Exit += delegate
        //    {
        //        repo.CommitTrace(Path.Combine(directory, "xlg-trace"));
        //    };

        //    return repo;
        //}

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
