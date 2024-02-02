using ClientTestSignalR_2.Enums;
using ClientTestSignalR_2.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ClientTestSignalR_2.Services
{
    /// <summary>
    /// сервис передачи сообщений на сервер посредством HubConnection
    /// </summary>
    class ConnectionServer : IConnectionService
    {
        private readonly IWriteMessageService writeMessageService;

        private readonly IMessageConverter messageConverter;

        #region == Constructor ==========================================================================================

        public ConnectionServer(IWriteMessageService writeMessageService, IMessageConverter messageConverter)
        {
            this.writeMessageService = writeMessageService;

            this.messageConverter = messageConverter;
            /*if (Program.host != null)
            {
                writeMessageListService = Program.host.Services.GetService<WriteMessageListService>(); //сервис для добавления сообщений в общий список
            }
            WriteMessageService = writeMessageService;*/
        }

        #endregion == Constructor ==

        #region == Fields ==========================================================================================

        //private readonly IConnectionService? _connectionService;

        /// <summary>
        /// текущее соединение
        /// </summary>
        HubConnection? connection;

        /// <summary>
        /// сервис добавления сообщения в чат
        /// </summary>
        //IWriteMessageService? writeMessageListService = null;

        #endregion == Fields ==
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

        public string? Nickname
        {
            get; set;
        }

        public StrConvertTypes StrConvertType
        {
            get; set;
        }

        /// <summary>
        /// установление соединения
        /// </summary>
        public async void Connect()
        {
            try
            {
                //создание подключения к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl($"{Address}")
                    .Build();

                // регистрация функции Receive для получения данных с сервера
                connection?.On<string, string>("Receive", (user, message) =>
                {
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                    /*    string? newMessage = $"{user}: {message}";

                        if (writeMessageService != null) //добавлние сообщения в чат
                        {
                            writeMessageService?.WriteMessage(MessageListObj, newMessage);
                        }
                    });*/
                        string newMessage = $"{user}: {message}";
                        if (writeMessageService != null) //добавлние сообщения в чат
                        {
                            writeMessageService?.WriteMessage(MessageListObj, newMessage);
                        }

                        if (user != Nickname && Nickname!=null)
                        {
                            string outputMessage = messageConverter.MessageConvert(StrConvertType, message);
                            SendMessage(Nickname, outputMessage);
                        }
                    });
            });

                if (connection != null)
                {
                    await connection.StartAsync();
                }

                writeMessageService?.WriteMessage(MessageListObj, "Соединение установлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// разрыв соединения
        /// </summary>
        public async void Disconnect()
        {
            //отключение
            if (connection != null)
            {
                await connection.StopAsync();
            }

            writeMessageService?.WriteMessage(MessageListObj, "Соединение отключено");
        }

        /// <summary>
        /// отправить сообщение
        /// </summary>
        /// <param name="nickname">ник пользователя</param>
        /// <param name="message">сообщение</param>
        public async void SendMessage(string nickname, string message)
        {
            try
            {
                if (connection != null)
                {
                    await connection.InvokeAsync("Send", nickname, message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}