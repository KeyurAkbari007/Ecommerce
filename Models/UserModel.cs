using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class SEC_UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IFormFile File { get; set; }
        public string? PhotoPath { get; set; }
        public bool? IsAdmin { get; set; }

    }
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
