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
    public class CreateUzytkownikModel : PageModel
    {
        private readonly ILogger<CreateUzytkownikModel> _logger;
        [BindProperty]
        public UzytkownikInfo Uzytkownik { get; set; }

        public string ErrorMessage { get; set; }

        public CreateUzytkownikModel(ILogger<CreateUzytkownikModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Akcje wykonywane przy pobieraniu strony Create.cshtml
        }

        public async Task<IActionResult> OnPostAsync([FromServices] UzytkownicyClient client, String tresc)
        {
            try
            {
                bool poprawneDane = false;
                string telefon = Uzytkownik.Telefon.ToString();
                string pesel = tresc;
                telefon = Regex.Replace(telefon, @"\D", ""); // \D - usuwanie wyszytkiego oprócz numerów
                if(Regex.IsMatch(telefon, @"^\d{9,11}$") && Regex.IsMatch(Uzytkownik.Mail, @"^[a-zA-Z0-9._]+@[a-zA-Z0-9]+.[a-zA-Z0-9]{2,}$")
                    && Regex.IsMatch(pesel, @"\d{11}$"))
                {
                    Uzytkownik.PESEL = tresc;
                    poprawneDane = true;
                }
                if((bool)poprawneDane)
                {
                    Uzytkownik.Rola = "-";
                    await client.CreateUzytkownikAsync(Uzytkownik);
                    return RedirectToPage("../Index");
                }
                else
                {
                    TempData["BladRejestracji"] = "Wype³nij poprawnie wszytskie pola";
                    return RedirectToPage("./Create");
                }
            }
            catch (Exception ex)
            {
                //obs³uga b³êdów przy braku elementów
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
