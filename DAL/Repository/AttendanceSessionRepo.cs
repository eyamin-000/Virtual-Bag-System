using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class AttendanceSessionRepo
    {
        VirtualBagDbContext db;

        public AttendanceSessionRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(AttendanceSession a)
        {
            db.AttendanceSessions.Add(a);
            return db.SaveChanges() > 0;
        }

        public AttendanceSession Get(int id)
        {
            return db.AttendanceSessions.Find(id);
        }

        public List<AttendanceSession> Get()
        {
            return db.AttendanceSessions.ToList();
        }

        public bool Update(AttendanceSession a)
        {
            var exobj = Get(a.SessionId);
            db.Entry(exobj).CurrentValues.SetValues(a);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.AttendanceSessions.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
