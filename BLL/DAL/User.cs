
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!!")]
        [StringLength(20, ErrorMessage = "User name must be maximum {1} characters!")] //you can also use 0, which is username
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!!")]
        [StringLength(10, ErrorMessage = "User name must be maximum {1} characters!")]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
