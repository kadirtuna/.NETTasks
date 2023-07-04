using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.DTO
{
    public class CreateUserDTO
    {
        public String Name { get; set; } = string.Empty;
        public String Surname { get; set; } = string.Empty;
        public String Username { get; set; } = string.Empty;
        public String Password { get; set; } = String.Empty;
        public String Email { get; set; } = string.Empty;
        public String Phone { get; set; } = string.Empty;
    }
}