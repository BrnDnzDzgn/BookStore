
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenreService : ServiceBase, IService<Genre, GenreModel>
    {
        public GenreService(Db db) : base(db)
        {
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.Include(p => p.BookGenres).ThenInclude(po => po.Book).OrderBy(o => o.Name).Select(o => new GenreModel() { Record = o });
        }

        public ServiceBase Create(Genre entity)
        {
            if (_db.Genres.Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim()))
                return Error("Genre with the same name exists!");

            entity.Name = entity.Name?.ToLower();
            _db.Genres.Add(entity);
            _db.SaveChanges();
            return Success("Genre created successfullly.");
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim()))
                return Error("Genre with the same name exists!");

            var entity = _db.Genres.Include(p => p.BookGenres).SingleOrDefault(p => p.Id == record.Id);
            if (entity == null)
                return Error("Pet not found!");

            _db.BookGenres.RemoveRange(entity.BookGenres); //removing previous ones

            entity.Name = record.Name?.ToLower();

            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genres updated successfullly.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(p => p.BookGenres).SingleOrDefault(p => p.Id == id);
            if (entity == null)
                return Error("Genre can not be found!");

            _db.BookGenres.RemoveRange(entity.BookGenres);

            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }

    }
}
