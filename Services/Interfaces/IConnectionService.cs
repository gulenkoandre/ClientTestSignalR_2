using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_2.Services.Interfaces
{
    /// <summary>
    /// сервис передачи сообщений
    /// </summary>
    public interface IConnectionService
    {
        /// <summary>
        /// адрес назначения соединения
        /// </summary>
        public string? Address
        {
            get; set;
        }

        /// <summary>
        /// объект лога сообщений, передаваемый через object 
        /// </summary>
        public object? MessageListObj
        {
            get; set;
        }

        /// <summary>
        /// запуск соединения
        /// </summary>
        public void Connect();

        /// <summary>
        /// разрыв соединения
        /// </summary>
        public void Disconnect();

        /// <summary>
        /// отправить сообщение
        /// </summary>
        /// <param name="nickname">ник пользователя</param>
        /// <param name="message">сообщение</param>
        public void SendMessage(string nickname, string message);
    }
}
