using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class BookModel
    {
        public Book Record { get; set; }

        [DisplayName("Name of the Book")]
        public string Name => Record.Name;

        [DisplayName("Number of Pages")]
        public string NumberOfPages => !Record.NumberOfPages.HasValue ? "0" : Record.NumberOfPages.Value.ToString("N1");

        [DisplayName("Publish Date")]
        public string PublishDate => Record.PublishDate.ToString("MM/dd/yyyy");

        public string Price => Record.Price.ToString("C2");

        [DisplayName("Top10 or Loser")]
        public string IsTopSeller => Record.IsTopSeller ? "In top 10" : "Loser Book";

        //for one to many (many side)
        public string Author => Record.Author?.Name + " " + Record.Author?.Surname;

        //many to many
        //for Details.cshtml
        //PetOwners must be inclued in the PetService query as an include since it is a navigational property
        public string Genres => string.Join("<br>", Record.BookGenres?.Select(po => po.Genre?.Name ));

        //For Create.cshtml
        public List<int> GenreIds
        {
            get => Record.BookGenres?.Select(po => po.GenreId).ToList();
            set => Record.BookGenres = value.Select(v => new BookGenre() { GenreId = v }).ToList();
        }

    }
}
