using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Models.Repository
{
    public interface IPassengerTypesRepository
    {
        IEnumerable<PassengerTypes> SelectAll();
    }
}