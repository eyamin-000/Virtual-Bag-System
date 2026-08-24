using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class HomeworkRepo
    {
        VirtualBagDbContext db;

        public HomeworkRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(Homework h)
        {
            db.Homeworks.Add(h);
            return db.SaveChanges() > 0;
        }

        public Homework Get(int id)
        {
            return db.Homeworks.Find(id);
        }

        public List<Homework> Get()
        {
            return db.Homeworks.ToList();
        }

        public bool Update(Homework h)
        {
            var exobj = Get(h.HomeworkId);
            db.Entry(exobj).CurrentValues.SetValues(h);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Homeworks.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
