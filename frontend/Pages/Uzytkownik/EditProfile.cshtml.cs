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
    public class EditProfileUzytkownikModel : PageModel
    {
        private readonly ILogger<EditProfileUzytkownikModel> _logger;
        [BindProperty]
        public UzytkownikInfo Uzytkownik { get; set; }

        public string ErrorMessage { get; set; }

        public EditProfileUzytkownikModel(ILogger<EditProfileUzytkownikModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromServices] UzytkownicyClient client)
        {
            Request.Cookies.TryGetValue("UserId", out string Id);
            int id = int.Parse(Id);
            try
            {
                Uzytkownik = await client.GetUzytkownikIdAsync(id);

                if (Uzytkownik == null)
                {
                    return NotFound();
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> OnPostAsync([FromServices] UzytkownicyClient client, int PhoneNumber, String Email)
        {
            Request.Cookies.TryGetValue("UserId", out string Id);
            int id = int.Parse(Id);
            try
            {
                Uzytkownik = await client.GetUzytkownikIdAsync(id);
                string telefon = PhoneNumber.ToString();
                telefon = Regex.Replace(telefon, @"\D", ""); // \D - usuwanie wyszytkiego oprócz numerów
                if (Regex.IsMatch(telefon, @"^\d{9,11}$") && Regex.IsMatch(Email, @"^[a-zA-Z0-9._]+@[a-zA-Z0-9]+.[a-zA-Z0-9]{2,}$"))
                {
                    Uzytkownik.Telefon = PhoneNumber;
                    Uzytkownik.Mail = Email;
                    await client.UpdateUzytkownikAsync(Uzytkownik, id);
                }
                else
                {
                    TempData["BladZmiany"] = "Wype³nij poprawnie wszytskie pola";
                }
                return RedirectToPage("./EditProfile");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
