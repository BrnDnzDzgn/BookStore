using BLL.DAL;

namespace BLL.Models
{
    public class AuthorModel
    {
        public Author Record { get; set; }

        public string NameSurname => Record.Name + " " + Record.Surname;

        public string Name => Record.Name;

        public string Surname => Record.Surname;

    }
}
