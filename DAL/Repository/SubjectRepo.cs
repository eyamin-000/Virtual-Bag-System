using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class SubjectRepo
    {
        VirtualBagDbContext db;

        public SubjectRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(Subject s)
        {
            db.Subjects.Add(s);
            return db.SaveChanges() > 0;
        }

        public Subject Get(int id)
        {
            return db.Subjects.Find(id);
        }

        public List<Subject> Get()
        {
            return db.Subjects.ToList();
        }

        public bool Update(Subject s)
        {
            var exobj = Get(s.SubjectId);

            db.Entry(exobj).CurrentValues.SetValues(s);

            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = Get(id);

            db.Subjects.Remove(exobj);

            return db.SaveChanges() > 0;
        }
    }
}
