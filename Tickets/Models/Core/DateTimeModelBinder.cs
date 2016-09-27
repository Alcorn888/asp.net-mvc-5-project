using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace Tickets.Models.Core
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        /// <summary>
        ///     Чтобы не было путаницы с форматом дат и локализацией, парсим дату по тому формату, который указан в атрибуте DataFormatString (если он есть)
        /// </summary>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                if (!String.IsNullOrEmpty(value.AttemptedValue))
                {
                    DateTime date;
                    displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);

                    if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} неверный формат даты", value.AttemptedValue)
                        );
                    }
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}