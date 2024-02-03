using ClientTestSignalR_2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ClientTestSignalR_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService<VM>();
        }

    }
}