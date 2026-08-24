using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class TeacherAssignmentRepo
    {
        VirtualBagDbContext db;

        public TeacherAssignmentRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(TeacherAssignment t)
        {
            db.TeacherAssignments.Add(t);
            return db.SaveChanges() > 0;
        }

        public TeacherAssignment Get(int id)
        {
            return db.TeacherAssignments.Find(id);
        }
        public List<TeacherAssignment> Get()
        {
            return db.TeacherAssignments.ToList();
        }

        public bool Update(TeacherAssignment t)
        {
            var exobj = Get(t.AssignmentId);

            db.Entry(exobj).CurrentValues.SetValues(t);

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);

            db.TeacherAssignments.Remove(exobj);

            return db.SaveChanges() > 0;
        }
    }
}
