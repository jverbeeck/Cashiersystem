using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.BLL;

namespace Website.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        [StringLength(5,ErrorMessage = "Exactly 5 numbers are allowed")]
        public string Password { get; set; }

        public string Salt { get; set; }
        public string CheckSum { get; set; }

        public int UserRole { get; set; }

        [NotMapped]
        public string UserRoleName => UserBLL.GetUserRoleName(UserRole);
    }
}