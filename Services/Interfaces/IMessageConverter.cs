using ClientTestSignalR_2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_2.Services.Interfaces
{
   public interface IMessageConverter
    {
        public string MessageConvert(StrConvertTypes convertType, string message);
    }
}
