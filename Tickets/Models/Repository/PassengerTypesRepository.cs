using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Models.Repository
{
    public class PassengerTypesRepository : IPassengerTypesRepository
    {
        private TicketsEntities db = null;

        public PassengerTypesRepository()
        {
            this.db = new TicketsEntities();
        }

        public PassengerTypesRepository(TicketsEntities db)
        {
            this.db = db;
        }

        public IEnumerable<PassengerTypes> SelectAll()
        {
            return db.PassengerTypes.ToList();
        }

    }
}