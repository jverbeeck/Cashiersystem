using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.BLL;

namespace Website.Models
{
    /// <summary>
    /// A user class
    /// </summary>
    public class User
    {
        /// <summary>
        /// The Id of this user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The first name of the user
        /// </summary>
        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the user
        /// </summary>
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }

        /// <summary>
        /// The Email address of the user
        /// </summary>
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        /// <summary>
        /// The phone number of the user
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The inserted password- not saved to the database like this
        /// </summary>
        [DataType(DataType.Password)]
        [NotMapped]
        [StringLength(5,ErrorMessage = "Exactly 5 numbers are allowed")]
        public string Password { get; set; }

        /// <summary>
        /// A generated key for encoding the password
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// The actual value that is saved to the database and is encoded with the salt 
        /// </summary>
        public string CheckSum { get; set; }

        /// <summary>
        /// Userrole of the user
        /// </summary>
        public int UserRole { get; set; }

        /// <summary>
        /// The userrole name of the user
        /// </summary>
        [NotMapped]
        public string UserRoleName => UserBLL.GetUserRoleName(UserRole);
    }
}