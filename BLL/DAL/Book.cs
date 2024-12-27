
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public short? NumberOfPages { get; set; }

        public DateTime PublishDate { get; set; }

        public decimal Price { get; set; }

        public bool IsTopSeller { get; set; }

        //for one to many (many side)
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        //for many to many (one side)
        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
