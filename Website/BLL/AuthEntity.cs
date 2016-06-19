namespace BLL.Extensions
{
    /// <summary>
    /// A helper class to determine id the user is authenticated and what his userrole is
    /// </summary>
    public class AuthEntity
    {
        /// <summary>
        /// Constructor with initial false values
        /// </summary>
        public AuthEntity()
        {
            IsAdmin = false;
            IsAuthenticated = false;
        }

        /// <summary>
        /// Boolean to determine wether the user is Admin
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Boolean to determine wether the user is authenticated
        /// </summary>
        public bool IsAuthenticated { get; set; }

    }
}
