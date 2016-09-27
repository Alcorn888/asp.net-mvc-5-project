using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tickets.Models.Core;

namespace Tickets.Models
{
    /// <summary>
    ///     При "model first" EF автоматически генерирует edmx с моделями, править их нельзя, поэтому назначение атрибутов вынесем в отдельные классы
    /// </summary>
    /// 
    [MetadataType(typeof(PassengersMetadata))]
    public partial class Passengers 
    {
        // расширим модель выпадающим списком с типами пассажиров
        public IEnumerable<System.Web.Mvc.SelectListItem> PassengerTypesSelectList { get; set; }
    }

    internal sealed class PassengersMetadata
    {
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public DateTime Birthday { get; set; }

        // раз Microsoft пихает регулярки C# в совершенно другой язык, javascript, то нужно подобрать такую, чтобы работала и там и там, поэтому p{IsCyrillic} выкидываем
        // [RegularExpression(@"[\p{IsCyrillic}\s-]+", ErrorMessage = "Имя может содержать только русские буквы, пробелы и дефисы")]
        [RegularExpression(@"[а-яА-ЯёЁ -]+", ErrorMessage = "Имя может содержать только русские буквы, пробелы и дефисы")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(255, ErrorMessage = "Значение не должно превышать 255 символов")]
        public string Name { get; set; }

        [CheckAgePassengerTypeAttribute("Birthday", ErrorMessage = "Тип пассажира должен соответствовать его возрасту")]
        [Required(ErrorMessage = "Выберите тип пассажира")]
        public int TypeID { get; set; }
    }
}