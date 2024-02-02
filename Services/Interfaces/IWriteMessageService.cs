using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_2.Services.Interfaces
{
    /// <summary>
    /// передача сообщения message на вывод посредством встроенных объектов obj, либо напрямую соответствующей реализацией WriteMessage
    /// </summary>
    public interface IWriteMessageService
    {
        public void WriteMessage(object? obj, string message);
    }
}
