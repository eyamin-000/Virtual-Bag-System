using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class HomeworkSubmissionRepo
    {
        VirtualBagDbContext db;

        public HomeworkSubmissionRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(HomeworkSubmission h)
        {
            db.HomeworkSubmissions.Add(h);
            return db.SaveChanges() > 0;
        }

        public HomeworkSubmission Get(int id)
        {
            return db.HomeworkSubmissions.Find(id);
        }

        public List<HomeworkSubmission> Get()
        {
            return db.HomeworkSubmissions.ToList();
        }

        public bool Update(HomeworkSubmission h)
        {
            var exobj = Get(h.SubmissionId);
            db.Entry(exobj).CurrentValues.SetValues(h);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.HomeworkSubmissions.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
