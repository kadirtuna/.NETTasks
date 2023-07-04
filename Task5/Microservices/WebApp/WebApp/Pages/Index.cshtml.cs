using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync() 
        { 
            if(!ModelState.IsValid)
            {
                return Page();
            }
            HttpClient httpClient = new HttpClient();
            ApiClient apiClient = new ApiClient();

            if (await apiClient.AuthenticateAsync(Username, Password) != null){
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid Username or password!";
                return Page();
            }
        }
    }
}