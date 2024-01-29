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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public OgloszenieInfo[] Ogloszenia { get; set; }
        public LokalizacjaInfo[] Lokalizacje { get; set; }
        public ZgloszenieInfo[] Zgloszenia { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }
        public UzytkownikInfo Uzytkownik { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromServices] UzytkownicyClient clientUzytkownicy, 
            [FromServices] ZgloszenieClient clientZgloszenie, [FromServices] LokalizacjaClient clientLokalizacja, 
            [FromServices] OgloszenieClient clientOgloszenie)
        {
            Ogloszenia = await clientOgloszenie.GetOgloszeniaAsync();
            Zgloszenia = await clientZgloszenie.GetOgloszeniaAsync();
            Lokalizacje = await clientLokalizacja.GetLokalizacjaAsync();
            Uzytkownicy = await clientUzytkownicy.GetUzytkownicyAsync();
            if (!(Uzytkownicy != null && Uzytkownicy.Any()))
            {
                UzytkownikInfo Uzytkownik = new UzytkownikInfo();
                Uzytkownik.Login = "Filip Makowiecki";
                Uzytkownik.Haslo = "haslo";
                Uzytkownik.PESEL = "72101572199";
                Uzytkownik.Rola = "Pracownik";
                Uzytkownik.Mail = "filip@FundacjaPomoc.com";
                Uzytkownik.Telefon = 274109376;
                await clientUzytkownicy.CreateUzytkownikAsync(Uzytkownik);
            }
            return Page();
        }
    }
}
