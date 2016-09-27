using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Models.Core
{
    /// <summary>
    ///     Вытаскиваем настройки из web.config
    /// </summary>
    public static class GetWebConfigSettings
    {
        /// <summary>
        ///     получим MVC Client Side Validation
        /// </summary>
        public static bool GetClientValidationEnabled()
        {
            bool result = true; // по умолчанию включен

            string property = System.Configuration.ConfigurationManager.AppSettings["ClientValidationEnabled"];

            if (property != null)
            {
                result = bool.TryParse(property, out result) ? result : true;
            }

            return result;
        }

        /// <summary>
        ///     получим флаг логгирования sql-запросов
        /// </summary>
        public static bool GetSqlDebugEnabled()
        {
            bool result = false; // по умолчанию выключен

            string property = System.Configuration.ConfigurationManager.AppSettings["SqlDebugEnabled"];

            if (property != null)
            {
                result = bool.TryParse(property, out result) ? result : false;
            }

            return result;
        }
    }
}