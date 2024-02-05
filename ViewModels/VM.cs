using ClientTestSignalR_2.Commands;
using ClientTestSignalR_2.Enums;
using ClientTestSignalR_2.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ClientTestSignalR_2.ViewModels
{
    public class VM : BaseVM
    {
        #region == Constructor ====================================================================================================
        public VM(IConnectionService connectionService, IMessageConverter messageConverter)
        {
            connectionServer = connectionService; 

            this.messageConverter = messageConverter; 
                        
            connectionServer.Address = $"{ServerAddress}{RequestPath}";

            connectionServer.Nickname = Nickname;

            connectionServer.MessageListObj = MessageList;

            connectionServer.StrConvertType = StrConverter;

            OutputMessage = messageConverter.MessageConvert(StrConverter, OutputMessage);            
        }
        #endregion == Constructor ==

        #region == Fields ====================================================================================================

        /// <summary>
        /// сервис для работы с сервером получаем в конструкторе класса
        /// </summary>
        private readonly IConnectionService connectionServer;

        /// <summary>
        /// сервис конвертации сообщений получаем в конструкторе класса
        /// </summary>
        private readonly IMessageConverter messageConverter;
        
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

                if (connectionServer != null)
                {
                    connectionServer.StrConvertType = StrConverter;
                }               
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
                if (connectionServer != null)
                {
                    connectionServer.Address = $"{ServerAddress}{RequestPath}";
                }
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
                if (connectionServer != null)
                {
                    connectionServer.Address = $"{ServerAddress}{RequestPath}";
                }
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
                if (connectionServer != null)
                {
                    connectionServer.Nickname = Nickname;
                }
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
            }
        }
        string _outputMessage = "Test";
        
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
                if (connectionServer != null)
                {
                    connectionServer.MessageListObj = MessageList;
                }

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

        /// <summary>
        /// по кнопке Очистить - очистка чата
        /// </summary>
        DelegateCommand? сommandClear;
        public ICommand CommandClear
        {
            get
            {
                if (сommandClear == null)
                {
                    сommandClear = new DelegateCommand(Clear);
                }
                return сommandClear;
            }

        }

        #endregion == Commands ==

        #region == Methods for Commands ===================================================================================================

        /// <summary>
        /// подключение к серверу
        /// </summary>
        /// <param name="obj"></param>
        private void Connect(object? obj)
        {   
                
            if (connectionServer != null)
                
            {                    
                connectionServer.Connect();

                ButtonConnectEnable = false;


                ButtonDisconnectEnable = true;


                StrConvertersEnable = true;
            }
            else
            {
                MessageBox.Show("Сервис подключения к серверу не активирован");
            }                    
        }

        /// <summary>
        /// отключение от сервера
        /// </summary>
        /// <param name="obj"></param>
        private void Disconnect(object? obj)
        {
                
            if (connectionServer != null)                
            {
                connectionServer.Disconnect();

                ButtonDisconnectEnable = false;

                ButtonConnectEnable = true;
                                   
                StrConvertersEnable = false;
            }
            else
            {
                MessageBox.Show("Сервис подключения к серверу не активирован");
            }          
        }

        /// <summary>
        /// очистка чата
        /// </summary>
        /// <param name="obj"></param>
        private void Clear(object? obj)
        {
            MessageList.Clear();
        }

        #endregion == Methods for Commands ==

        #region == Methods ===================================================================================================
        
        /// <summary>
        /// отправить сообщение серверу
        /// </summary>
        /// <param name="obj"></param>
        private void SendMessage()
        {
            //отправка сообщения на сервер                
            if (connectionServer != null)                
            {                    
                connectionServer.SendMessage(Nickname, OutputMessage);
            }                
            else                
            {                    
                MessageBox.Show("Сервис подключения к серверу не активирован");
            }            
        }
        #endregion == Methods ==

    }
}
