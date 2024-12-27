
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        //in one to many (one side)
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
