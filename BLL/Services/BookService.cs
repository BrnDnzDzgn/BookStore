
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class BookService : ServiceBase, IService<Book, BookModel>
    {
        public BookService(Db db) : base(db)
        {
        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books.Include(b => b.Author).Include(p => p.BookGenres).ThenInclude(po => po.Genre).OrderBy(b => b.Name).Select(b => new BookModel() { Record = b });
        }

        public ServiceBase Create(Book entity)
        {
            if (_db.Books.Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim()))
                return Error("Book with the same name!");

            entity.Name = entity.Name?.ToLower();
            _db.Books.Add(entity);
            _db.SaveChanges();
            return Success("Book created successfullly.");
        }

        public ServiceBase Update(Book record)
        {
            if (_db.Books.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim()))
                return Error("Book with the same name exists!");

            var entity = _db.Books.Include(p => p.BookGenres).SingleOrDefault(p => p.Id == record.Id);
            if (entity == null)
                return Error("Book not found!");

            _db.BookGenres.RemoveRange(entity.BookGenres); //removing previous ones

            entity.Name = record.Name?.ToLower();         
            entity.NumberOfPages = record.NumberOfPages;            
            entity.PublishDate = record.PublishDate;            
            entity.Price = record.Price;            
            entity.IsTopSeller = record.IsTopSeller;            
            entity.AuthorId = record.AuthorId;            
            entity.BookGenres = record.BookGenres;
            entity.Author = record.Author; //added this

            _db.Books.Update(entity);
            _db.SaveChanges();
            return Success("Book updated successfullly.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Books.Include(p => p.BookGenres).SingleOrDefault(p => p.Id == id);
            if (entity == null)
                return Error("Book can not be found!");

            _db.BookGenres.RemoveRange(entity.BookGenres);

            _db.Books.Remove(entity);
            _db.SaveChanges();
            return Success("Book deleted successfully.");
        }
    }
}
