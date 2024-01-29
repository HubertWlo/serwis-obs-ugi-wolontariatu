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
    public class IndexOgloszenieModel : PageModel
    {
        private readonly ILogger<IndexOgloszenieModel> _logger;
        public OgloszenieInfo[] Ogloszenia { get; set; }
        public LokalizacjaInfo[] Lokalizacje { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }

        public string ErrorMessage { get; set; }

        public IndexOgloszenieModel(ILogger<IndexOgloszenieModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] OgloszenieClient clientOgloszenie, [FromServices] UzytkownicyClient clientUzytkownik, [FromServices] LokalizacjaClient clientLokalizacja)
        {
            Ogloszenia = await clientOgloszenie.GetOgloszeniaAsync();
            Uzytkownicy = await clientUzytkownik.GetUzytkownicyAsync();
            Lokalizacje = await clientLokalizacja.GetLokalizacjaAsync();
        }
    }
}
