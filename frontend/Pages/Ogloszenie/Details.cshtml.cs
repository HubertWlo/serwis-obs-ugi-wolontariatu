using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages.Ogloszenie
{
    public class DetailsOgloszenieModel : PageModel
    {
        private readonly ILogger<DetailsOgloszenieModel> _logger;
        [BindProperty]
        public OgloszenieInfo Ogloszenie { get; set; }
        public LokalizacjaInfo[] Lokalizacja { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }
        public ZgloszenieInfo Zgloszenie { get; set; }
        public ZgloszenieInfo[] Zgloszenia { get; set; }

        public string ErrorMessage { get; set; }

        public DetailsOgloszenieModel(ILogger<DetailsOgloszenieModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] OgloszenieClient client, [FromServices] LokalizacjaClient clientLokalizacja, 
            [FromServices] UzytkownicyClient clientUzytkownik, [FromServices] ZgloszenieClient clientZgloszenie, int id)
        {
            Lokalizacja = await clientLokalizacja.GetLokalizacjaAsync();
            Uzytkownicy = await clientUzytkownik.GetUzytkownicyAsync();
            Ogloszenie = await client.GetOgloszenieIdAsync(id);
            Zgloszenia = await clientZgloszenie.GetOgloszeniaAsync();
            Zgloszenie = Zgloszenia.FirstOrDefault(z => z.WolontariuszId == Ogloszenie.WolontariuszId && z.OgloszenieId == Ogloszenie.Id);
            try
            {
                Ogloszenie = await client.GetOgloszenieIdAsync(id);

                if (Ogloszenie == null)
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
    }
}
