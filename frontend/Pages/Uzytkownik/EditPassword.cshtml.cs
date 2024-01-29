using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class EditPasswordUzytkownikModel : PageModel
    {
        private readonly ILogger<EditPasswordUzytkownikModel> _logger;
        [BindProperty]
        public UzytkownikInfo Uzytkownik { get; set; }

        public string ErrorMessage { get; set; }

        public EditPasswordUzytkownikModel(ILogger<EditPasswordUzytkownikModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Akcje wykonywane przy pobieraniu strony Create.cshtml
        }

        public async Task<IActionResult> OnPostAsync([FromServices] UzytkownicyClient client, String OldPassword, 
            String NewPassword, String ConfirmPassword)
        {
            Request.Cookies.TryGetValue("UserId", out string Id);
            int id = int.Parse(Id);
            try
            {
                Uzytkownik = await client.GetUzytkownikIdAsync(id);
                if (NewPassword == ConfirmPassword && OldPassword == Uzytkownik.Haslo)
                {
                    Uzytkownik.Haslo = NewPassword;
                    await client.UpdateUzytkownikAsync(Uzytkownik, id);
                }
                else if(NewPassword !=  ConfirmPassword)
                {
                    TempData["BladHasla"] = "Podane has³a nie s¹ zgodne";
                }
                else
                {
                    TempData["BladHasla"] = "Niepoprawne stare has³o";
                }
                return RedirectToPage("./EditPassword");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
