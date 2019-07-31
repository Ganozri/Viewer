using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Peleng.Medusa.Common.Translation;

using static System.Array;
using Path = System.IO.Path;

namespace Peleng.Medusa.Analyze1553B
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IServiceProvider serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            serviceProvider.GetService<MainWindow>().Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<IMainWindowViewModel, MainWindowViewModel>();

            services.AddSingleton(_ => LoadTranslation());
        }

        private TranslationRepository LoadTranslation()
        {
            TranslationRepository repo = new TranslationRepository();
            var directory = System.IO.Path.GetDirectoryName(typeof(App).Assembly.Location) ?? Environment.CurrentDirectory;
            var files = Directory.GetFiles(directory, "*.xlg", SearchOption.AllDirectories);
            ForEach(files, x => repo.AddTranslationFile(x));

            repo.AllowTrace(true);
            this.Exit += delegate
            {
                repo.CommitTrace(Path.Combine(directory, "xlg-trace"));
            };

            return repo;
        }
    }
}
