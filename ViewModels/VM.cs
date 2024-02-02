using ClientTestSignalR_2.Commands;
using ClientTestSignalR_2.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ClientTestSignalR_2.ViewModels
{
    public class VM : BaseVM
    {
        #region == Constructor ====================================================================================================
        public VM()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        #endregion == Constructor ==

        #region == Fields ====================================================================================================

        HubConnection? connection; // объявляем подключение для работы с сервером (хабом)

        Dispatcher _dispatcher; // для работы с элементами WPF в главном потоке

        #endregion == Fields ==

        #region == Properties ================================================================================================

        /// <summary>
        /// типы преобразования строки для выбора в ComboBox
        /// </summary>
        public ObservableCollection<StrConvertTypes> StrConverters {get; set;} = new ObservableCollection<StrConvertTypes>()
        {
                StrConvertTypes.Backwards,

                StrConvertTypes.Upper,

                StrConvertTypes.Lower,

                StrConvertTypes.Random

        };

        public StrConvertTypes StrConverter
        {
            get => _strConverter;

            set
            {
                _strConverter = value;
                
                OnPropertyChanged(nameof(StrConverter));

                OutputMessage = MessageConvert(OutputMessage);
                
                SendMessage();
            }
        }
        StrConvertTypes _strConverter = StrConvertTypes.Random;

        public bool StrConvertersEnable
        {
            get => _strConvertersEnable;

            set
            {
                _strConvertersEnable = value;
                OnPropertyChanged(nameof(StrConvertersEnable));
            }
        }
        bool _strConvertersEnable = false;

        /*public bool ButtonSendEnable
        {
            get => _buttonSendEnable;

            set
            {
                _buttonSendEnable = value;
                OnPropertyChanged(nameof(ButtonSendEnable));
            }
        }
        bool _buttonSendEnable = false;*/

        public bool ButtonConnectEnable
        {
            get => _buttonConnectEnable;

            set
            {
                _buttonConnectEnable = value;
                OnPropertyChanged(nameof(ButtonConnectEnable));
            }
        }
        bool _buttonConnectEnable = true;

        public bool ButtonDisconnectEnable
        {
            get => _buttonDisconnectEnable;

            set
            {
                _buttonDisconnectEnable = value;
                OnPropertyChanged(nameof(ButtonDisconnectEnable));
            }
        }
        bool _buttonDisconnectEnable = false;

        /// <summary>
        /// Путь запроса (в формате /str)
        /// </summary>
        public string RequestPath
        {
            get => _requestPath;

            set
            {
                _requestPath = value;
                OnPropertyChanged(nameof(RequestPath));
            }
        }
        string _requestPath = "/str";

        /// <summary>
        /// Адрес сервера (в формате https://localhost:7018)
        /// </summary>
        public string ServerAddress
        {
            get => _serverAddress;

            set
            {
                _serverAddress = value;
                OnPropertyChanged(nameof(ServerAddress));
            }
        }
        string _serverAddress = "https://localhost:7018";


        /// <summary>
        /// имя в чате
        /// </summary>
        public string Nickname
        {
            get => _nickname;

            set
            {
                _nickname = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }
        string _nickname = "AntiChat";

        /// <summary>
        /// исходящее сообщение
        /// </summary>
        public string OutputMessage
        {
            get => _outputMessage;

            set
            {
                _outputMessage = value;
                //OnPropertyChanged(nameof(OutputMessage));
            }
        }
        string _outputMessage = "Test";

        /// <summary>
        /// входящее сообщение
        /// </summary>
        public string InputMessage
        {
            get => _inputMessage;

            set
            {
                _inputMessage = value;
                OnPropertyChanged(nameof(InputMessage));
            }
        }
        string _inputMessage = "";

        // <summary>
        /// история сообщений
        /// </summary>
        public ObservableCollection<string> MessageList
        {
            get => _messageList;

            set
            {
                _messageList = value;
                OnPropertyChanged(nameof(MessageList));
            }
        }
        ObservableCollection<string> _messageList = new ObservableCollection<string>();

        #endregion == Properties ==

        #region == Commands ===================================================================================================

        DelegateCommand? commandConnect;
        public ICommand CommandConnect
        {
            get
            {
                if (commandConnect == null)
                {
                    commandConnect = new DelegateCommand(Connect);
                }
                return commandConnect;
            }

        }

        DelegateCommand? commandDisconnect;
        public ICommand CommandDisconnect
        {
            get
            {
                if (commandDisconnect == null)
                {
                    commandDisconnect = new DelegateCommand(Disconnect);
                }
                return commandDisconnect;
            }

        }

        /*DelegateCommand? commandSendMessage;
        public ICommand CommandSendMessage
        {
            get
            {
                if (commandSendMessage == null)
                {
                    commandSendMessage = new DelegateCommand(SendMessageCommand);
                }
                return commandSendMessage;
            }

        }*/

        #endregion == Commands ==

        #region == Methods for Commands ===================================================================================================

        private async void Connect(object? obj)
        {
            OpenConnectionServer(ServerAddress, RequestPath);

            try
            {
                ButtonConnectEnable = false;

                //подключение к хабу
                if (connection != null)
                {
                    await connection.StartAsync();
                }

                MessageList.Add("Соединение установлено");

                StrConvertersEnable = true;

                ButtonDisconnectEnable = true;
            }
            catch (Exception ex)
            {
                ButtonConnectEnable = true;

                MessageBox.Show(ex.Message);
            }
        }

        private async void Disconnect(object? obj)
        {
            try
            {
                ButtonDisconnectEnable = false;

                //подключение к хабу
                if (connection != null)
                {
                    await connection.StopAsync();
                }

                MessageList.Add("Соединение отключено");

                ButtonConnectEnable = true;

                StrConvertersEnable = false;
            }
            catch (Exception ex)
            {
                ButtonDisconnectEnable = true;

                MessageBox.Show(ex.Message);
            }
        }

        /*private void SendMessageCommand(object? obj)
        {
            SendMessage();            
        }*/

        #endregion == Methods for Commands ==

        #region == Methods ===================================================================================================

        /// <summary>
        /// Открыть соединение с сервером
        /// </summary>
        /// <param name="serverAddress">в формате "https://localhost:7018" </param>
        /// <param name="requestPath"> путь запроса в формате "/chat" </param>
        private void OpenConnectionServer(string serverAddress, string requestPath) // serverAddress в формате "https://localhost:7018", путь запроса в формате "/str"
        {
            //создание подключения к хабу
            connection = new HubConnectionBuilder()
                .WithUrl($"{serverAddress}{requestPath}")
                .Build();

            // регистрация функции Receive для получения данных
            connection.On<string, string>("Receive", (user, message) =>
            {
                _dispatcher.Invoke(() =>
                {
                    string newMessage = $"{user}: {message}";
                    MessageList.Add(newMessage);
                    if (user != Nickname)
                    {
                        OutputMessage = MessageConvert(message);
                        SendMessage();
                    }
                });
            });
        }

        /// <summary>
        /// отправить сообщение
        /// </summary>
        private async void SendMessage()
        {
            try
            {
                //отправка сообщения
                if (connection != null)
                {
                    await connection.InvokeAsync("Send", Nickname, OutputMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string MessageConvert(string message)
        {
            switch (StrConverter)
            {
                case StrConvertTypes.Backwards: //переворот строки
                    {
                        char[] charArray = message.ToCharArray();
                        Array.Reverse(charArray);
                        return new string(charArray);
                    }                    

                case StrConvertTypes.Upper: //в верхний регистр
                    {
                        return message.ToUpper();
                    }                    

                case StrConvertTypes.Lower: //в нижний регистр
                    {
                        return message.ToLower(); 
                    }
                
                default: //case StrConvertTypes.Random - формирование случайной строки
                    {
                        return RandomString (message);
                    }                    
            }
                       
        }

        /// <summary>
        /// получение случайной строки той же длины, что и отправлена от клиента Chat
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string RandomString(string message)
        {
            Random rnd;

            int randomNum;

            char[] charArray = message.ToCharArray();
            
            for (int i = 0; i < message.Count(); i++)
            {
                rnd = new Random();

                randomNum = rnd.Next(1, 107);

                charArray[i] = (char)randomNum;

            }
            
            return new string(charArray);
        }

        #endregion == Methods ==

    }
}
