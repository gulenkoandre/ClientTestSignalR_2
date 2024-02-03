using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_2.Enums
{
    /// <summary>
    /// список типов конвертации для выбора обработки строки
    /// </summary>
    public enum StrConvertTypes
    {
        Backwards, //задом - наперед

        Upper, //верхний регистр

        Lower, //нижний регистр

        Random //случайная строка

    }
}
