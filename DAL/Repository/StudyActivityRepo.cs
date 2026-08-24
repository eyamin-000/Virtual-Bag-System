using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class StudyActivityRepo
    {
        VirtualBagDbContext db;

        public StudyActivityRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(StudyActivity s)
        {
            db.StudyActivities.Add(s);
            return db.SaveChanges() > 0;
        }

        public StudyActivity Get(int id)
        {
            return db.StudyActivities.Find(id);
        }

        public List<StudyActivity> Get()
        {
            return db.StudyActivities.ToList();
        }

        public bool Update(StudyActivity s)
        {
            var exobj = Get(s.ActivityId);
            db.Entry(exobj).CurrentValues.SetValues(s);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.StudyActivities.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
