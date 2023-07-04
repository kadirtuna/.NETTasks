using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.DTO
{
    public class AuthenticationDTO
    {
        public String Username { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
    }
}
