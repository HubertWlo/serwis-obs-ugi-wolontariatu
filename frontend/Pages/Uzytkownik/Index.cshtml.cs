using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class IndexUzytkownikModel : PageModel
    {
        private readonly ILogger<IndexUzytkownikModel> _logger;
        public UzytkownikInfo[] Uzytkownicy { get; set; }
        public UzytkownikInfo Uzytkownik { get; set; }
        public UzytkownikInfo test { get; set; }

        public string ErrorMessage { get; set; }

        public IndexUzytkownikModel(ILogger<IndexUzytkownikModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] UzytkownicyClient client)
        {
            test = (UzytkownikInfo)HttpContext.Items["User"];
            Uzytkownicy = await client.GetUzytkownicyAsync();

            if (Uzytkownicy.Count() == 0)
                ErrorMessage = "We must be sold out. Try again tomorrow.";
            else
                ErrorMessage = string.Empty;
        }
        public async Task<IActionResult> OnPostAsync([FromServices] UzytkownicyClient client, int id, String uprawnienie)
        {
            try
            {
                UzytkownikInfo Uzytkownik = new UzytkownikInfo();
                Uzytkownik = await client.GetUzytkownikIdAsync(id);
                Uzytkownik.Rola = uprawnienie;
                await client.UpdateUzytkownikAsync(Uzytkownik, Uzytkownik.Id);
                return RedirectToPage("/Uzytkownik/Index"); // Przekierowanie po pomyślnym utworzeniu ogłoszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
