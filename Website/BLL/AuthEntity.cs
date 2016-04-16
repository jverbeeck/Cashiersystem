using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public class AuthEntity
    {
        public AuthEntity()
        {
            IsAdmin = false;
            IsAuthenticated = false;
        }

        public bool IsAdmin { get; set; }
        public bool IsAuthenticated { get; set; }

    }
}
