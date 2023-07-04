using Microsoft.AspNetCore.Identity;

namespace WebApp.DTO
{
    public class ApplicationUser: IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
