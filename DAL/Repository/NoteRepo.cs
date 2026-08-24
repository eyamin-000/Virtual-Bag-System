using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class NoteRepo
    {
        VirtualBagDbContext db;

        public NoteRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(Note n)
        {
            db.Notes.Add(n);
            return db.SaveChanges() > 0;
        }

        public Note Get(int id)
        {
            return db.Notes.Find(id);
        }

        public List<Note> Get()
        {
            return db.Notes.ToList();
        }

        public bool Update(Note n)
        {
            var exobj = Get(n.NoteId);
            db.Entry(exobj).CurrentValues.SetValues(n);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Notes.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
}
