using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class BookRepo
    {
        VirtualBagDbContext db;

        public BookRepo(VirtualBagDbContext db)
        {
            this.db = db;
        }

        public bool Create(Book b)
        {
            db.Books.Add(b);
            return db.SaveChanges() > 0;
        }

        public Book Get(int id)
        {
            return db.Books.Find(id);
        }

        public List<Book> Get()
        {
            return db.Books.ToList();
        }

        public bool Update(Book b)
        {
            var exobj = Get(b.BookId);

            db.Entry(exobj).CurrentValues.SetValues(b);

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);

            db.Books.Remove(exobj);

            return db.SaveChanges() > 0;
        }
    }
}
