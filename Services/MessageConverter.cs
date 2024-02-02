using ClientTestSignalR_2.Enums;
using ClientTestSignalR_2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_2.Services
{
    public class MessageConverter : IMessageConverter
    {
        public string MessageConvert(StrConvertTypes convertType, string message)
        {
            switch (convertType)
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
                        return RandomString(message);
                    }
            }

        }

        /// <summary>
        /// получение случайной строки
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string RandomString(string message)
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
    }
}
