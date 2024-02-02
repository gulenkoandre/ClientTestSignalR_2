using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
