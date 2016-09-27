using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Models.Repository
{
    public interface IPassengersRepository
    {
        IEnumerable<Passengers> SelectAll();
        Passengers SelectByID(int id);
        void Insert(Passengers obj);
        void Update(Passengers obj);
        void Delete(int id);
        void Save();
    }
}