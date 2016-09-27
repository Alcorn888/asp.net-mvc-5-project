using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Models.Core
{
    /// <summary>
    ///     Все настройки приложения
    /// </summary>
    public static class Settings
    {
        // asp.net mvc клиентская валидация
        public static bool ClientValidationEnabled = GetWebConfigSettings.GetClientValidationEnabled();
        // логгирование sql-запросов
        public static bool SqlDebugEnabled = GetWebConfigSettings.GetSqlDebugEnabled();
        // другие настройки
    }
}
