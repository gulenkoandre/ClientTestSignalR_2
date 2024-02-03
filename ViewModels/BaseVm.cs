using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ClientTestSignalR_2.ViewModels
{
    /// <summary>
    /// базовый  класс для ViewModel в рамках MVVM
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
