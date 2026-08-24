using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class ClassRepo
    {
        VirtualBagDbContext db;

        public ClassRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(Class c)
        {
            db.Classes.Add(c);
            return db.SaveChanges() > 0;
        }

        public Class Get(int id)
        {
            return db.Classes.Find(id);
        }

        public List<Class> Get()
        {
            return db.Classes.ToList();
        }

        public bool Update(Class c)
        {
            var exobj = Get(c.ClassId);

            db.Entry(exobj).CurrentValues.SetValues(c);

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);

            db.Classes.Remove(exobj);

            return db.SaveChanges() > 0;
        }
    }
}
