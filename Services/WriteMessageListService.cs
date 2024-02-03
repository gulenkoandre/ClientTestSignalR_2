using ClientTestSignalR_2.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClientTestSignalR_2.Services
{
    /// <summary>
    /// передача сообщения message ListBox с указанием даты и времени
    /// </summary>
    public class WriteMessageListService : IWriteMessageService
    {
        public void WriteMessage(object? obj, string message)
        {
            try
            {
                //обрабатываем в главном потоке
                App.Current.Dispatcher.Invoke(() =>
                {

                    if (obj != null)
                    {
                        ((ObservableCollection<string>)obj).Add($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}/ {message}");
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
