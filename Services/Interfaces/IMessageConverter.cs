using ClientTestSignalR_2.Enums;


namespace ClientTestSignalR_2.Services.Interfaces
{
    /// <summary>
    /// сервис конвертации сообщений
    /// </summary>
    public interface IMessageConverter
    {
        public string MessageConvert(StrConvertTypes convertType, string message);
    }
}
