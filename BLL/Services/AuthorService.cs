
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AuthorService : ServiceBase, IService<Author, AuthorModel>
    {
        public AuthorService(Db db) : base(db)
        {
        }

        public IQueryable<AuthorModel> Query()
        {
            return _db.Authors.OrderBy(s => s.Name).ThenBy(o => o.Surname).Select(s => new AuthorModel { Record = s });
        }

        public ServiceBase Create(Author record)
        {
            if (_db.Authors.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim() &&
                s.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Author with the same name and surname already exists");

            record.Name = record.Name?.Trim();
            record.Surname = record.Surname?.Trim();
            _db.Authors.Add(record);
            _db.SaveChanges();
            return Success("Author created successfully.");
        }

        public ServiceBase Update(Author record)
        {
            if (_db.Authors.Any(s => s.Id != record.Id && 
                                s.Name.ToUpper() == record.Name.ToUpper().Trim() &&
                                s.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Author with the same name and surname already exists");

            //get the entity from the database first, because to update you need to first get it from the database
            var entity = _db.Authors.SingleOrDefault(s => s.Id == record.Id);
            if (entity == null)
                return Error("Author can not be found");
            
            entity.Name = record.Name?.Trim();
            entity.Surname = record.Surname?.Trim();
            _db.Authors.Update(entity);
            _db.SaveChanges();
            return Success("Author updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Authors.Include(s => s.Books).SingleOrDefault(s => s.Id == id);
            if (entity == null)
                return Error("Author can not be found");

            if (entity.Books.Count() > 0) //or just use Any()
                return Error("Author has relational Books! Delete them first!");
            //since we are using Books navigational property above, we need to add Include(s => s.Books) above above

            _db.Authors.Remove(entity);
            _db.SaveChanges();
            return Success("Author deleted successfully.");
        }

    }
}
