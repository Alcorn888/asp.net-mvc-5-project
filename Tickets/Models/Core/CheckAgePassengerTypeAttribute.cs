using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Tickets.Models.Core
{
    public class CheckAgePassengerTypeAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string _otherProperty;
        public CheckAgePassengerTypeAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_otherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    "Unknown property {0}",
                    new[] { _otherProperty }
                ));
            }
            var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyValue != null || otherPropertyValue as string != string.Empty)
            {
                if (value != null || value as string != string.Empty)
                {
                    bool isError = false;

                    DateTime birthday = (DateTime)otherPropertyValue;

                    int typePassenger = (int)value;

                    // проверим дату рождения и тип пассажира
                    if (
                        (typePassenger == 2 && birthday.AddYears(10) > DateTime.Now) // старше 10 лет
                        ||
                        (typePassenger == 3 && (birthday.AddYears(5) > DateTime.Now || birthday.AddYears(10) < DateTime.Now)) // от 5 до 10 лет
                        ||
                        (typePassenger == 4 && birthday.AddYears(5) < DateTime.Now) // до 5 лет
                    )
                    {
                        isError = true;
                    }

                    if (isError)
                    {
                        return new ValidationResult(string.Format(
                            CultureInfo.CurrentCulture,
                            FormatErrorMessage(validationContext.DisplayName),
                            new[] { _otherProperty }
                        ));
                    }
                }
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "checkage",
            };
            rule.ValidationParameters.Add("other", _otherProperty);
            yield return rule;
        }
    }
}