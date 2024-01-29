using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Uzytkownik
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Delete("UserRole");
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserLogin");
            return  RedirectToPage("/Index");
        }
    }
}
