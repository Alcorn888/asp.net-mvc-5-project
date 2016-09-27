using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Models.Repository
{
    public class PassengersRepository : IPassengersRepository
    {
        private TicketsEntities db = null;
 
        public PassengersRepository()
        {
            this.db = new TicketsEntities();
        }

        public PassengersRepository(TicketsEntities db)
        {
            this.db = db;
        }
 
        public IEnumerable<Passengers> SelectAll()
        {
            return db.Passengers.ToList();
        }
 
        public Passengers SelectByID(int id)
        {
            return db.Passengers.Find(id);
        }
 
        public void Insert(Passengers obj)
        {
            db.Passengers.Add(obj);
        }
 
        public void Update(Passengers obj)
        {
            //db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

            // поле Created записывается при добавлении записи и больше никогда не меняется, поэтому выкинем его из sql-запроса на update
            var excluded = new[] { "Created" };

            db.Passengers.Attach(obj);

            var entry = db.Entry(obj);
            
            foreach (var name in entry.CurrentValues.PropertyNames.Except(excluded))
            {
                entry.Property(name).IsModified = true;
            }
        }
 
        public void Delete(int id)
        {
            Passengers existing = db.Passengers.Find(id);

            db.Passengers.Remove(existing);
        }
 
        public void Save()
        {
            // пишем sql-запрос в лог, если нужно
            if (Core.Settings.SqlDebugEnabled)
            {
                string path = HttpContext.Current.Request.PhysicalApplicationPath + @"Logs\sqlLog.txt";

                using (var sqlLogFile = new System.IO.StreamWriter(path))
                {
                    db.Database.Log = sqlLogFile.Write;
                    db.SaveChanges();
                }
            }
            else
            {
                db.SaveChanges();
            }
        }

    }
}