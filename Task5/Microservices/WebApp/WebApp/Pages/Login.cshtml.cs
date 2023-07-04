using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Services.Shared.DTO;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.Services;

namespace WebApp.Pages
{
    // [Authorize]
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;

        public LoginModel(ILogger<LoginModel> logger, IConfiguration configuration, ApiClient apiClient)
        {
            _logger = logger;
            _configuration = configuration;
            _apiClient = apiClient;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //bool isValidUser = this.Username == "kadirtuna" && this.Password == "12345";
                string token = await _apiClient.AuthenticateAsync(this.Username, this.Password);

                if (token != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["Jwt:Issuer"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    ClaimsPrincipal claimsPrincipal;
                    try
                    {
                        claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                    }
                    catch (Exception ex)
                    {
                        // Token validation failed
                        return BadRequest(ex.Message);
                    }

                    // Get the identity from the claims principal
                    var identity = claimsPrincipal.Identity as ClaimsIdentity;

                    // Get the user ID claim from the identity
                    var username = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                    var user = new ClaimsPrincipal(identity);

                    //var authProperties = new AuthenticationProperties
                    //{
                    //    IsPersistent = true,
                    //    //RedirectUri = returnUrl,
                    //    // Specify the authentication scheme here
                    //    AllowRefresh = true,
                    //    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                    //    IssuedUtc = DateTimeOffset.UtcNow
                    //};

                    await HttpContext.SignInAsync("Cookies", claimsPrincipal);
                    return RedirectToPage("/Index");

                }

            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            await HttpContext.SignOutAsync(scheme);

            return RedirectToPage("/Index");
        }
    }
}