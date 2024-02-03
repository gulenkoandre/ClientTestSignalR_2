

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
