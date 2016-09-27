using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;

namespace Tickets.Models.Core
{
    ///
    /// Здесь расширения
    ///
    static class ListExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
                  this IEnumerable<PassengerTypes> items, int selectedId)
        {
            return
                items.OrderBy(item => item.id)
                      .Select(item =>
                          new SelectListItem
                          {
                              Selected = (item.id == selectedId),
                              Text = item.Name,
                              Value = item.id.ToString()
                          });
        }
    }
}