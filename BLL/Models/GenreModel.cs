
using BLL.DAL;

namespace BLL.Models
{
    public class GenreModel
    {
        public Genre Record { get; set; }

        public string Name => Record.Name;

        //many to many
        //for details.cshtml
        //BookGenres must be inclued in the GenreService query as an include since it is a navigational property
        public string Books => string.Join("<br>", Record.BookGenres?.Select(po => po.Book?.Name));

        //For Create.cshtml
        public List<int> BookIds
        {
            get => Record.BookGenres?.Select(po => po.BookId).ToList();
            set => Record.BookGenres = value.Select(v => new BookGenre() { BookId = v }).ToList();
        }
    }
}
