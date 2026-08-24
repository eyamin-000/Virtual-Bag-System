using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class AttendanceRepo
    {
        VirtualBagDbContext db;

        public AttendanceRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(Attendance a)
        {
            db.Attendances.Add(a);
            return db.SaveChanges() > 0;
        }

        public Attendance Get(int id)
        {
            return db.Attendances.Find(id);
        }

        public List<Attendance> Get()
        {
            return db.Attendances.ToList();
        }

        public bool Update(Attendance a)
        {
            var exobj = Get(a.AttendanceId);
            db.Entry(exobj).CurrentValues.SetValues(a);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Attendances.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
