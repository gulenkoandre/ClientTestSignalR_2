// Ignore Spelling: App

using ClientTestSignalR_2.Services;
using ClientTestSignalR_2.Services.Interfaces;
using ClientTestSignalR_2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Xaml;

namespace ClientTestSignalR_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly MainWindow? mainWindow;
        
        // в конструкторе через систему внедрения зависимостей получаем объект главного окна
        public App()
        {
            Services = ConfigureServices();

            mainWindow = App.Current.Services.GetService<MainWindow>();

            //this.InitializeComponent();
        }
        
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            
            services.AddSingleton<MainWindow>(); //например для доступа из других мест программы: MainWindow? mainWindow = App.Current.Services.GetService<MainWindow>();

            //services.AddSingleton<App>();

            //Services
            services.AddSingleton<IWriteMessageService, WriteMessageListService>();

            services.AddSingleton<IConnectionService, ConnectionServer>(); //для получения - IConnectionService? сonnectionService = App.Current.Services.GetService<IConnectionService>();

            //ViewModels
            services.AddTransient<VM>();

            return services.BuildServiceProvider();


        }

        //OnStartup - метод который запускается при старте приложения
        protected override void OnStartup(StartupEventArgs e)
        {
            if (mainWindow != null)
            {
                mainWindow.Show();  // отображаем главное окно на экране
            }
            
            base.OnStartup(e);
        }
    }

    
}
