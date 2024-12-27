
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        //for many to many (one side)
        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
