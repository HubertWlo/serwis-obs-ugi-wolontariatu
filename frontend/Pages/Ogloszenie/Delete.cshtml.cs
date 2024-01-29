using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Ogloszenie
{
    public class DeleteOgloszenieModel : PageModel
    {
        private readonly ILogger<DeleteOgloszenieModel> _logger;
        public OgloszenieInfo Ogloszenie { get; set; }
        public LokalizacjaInfo[] Lokalizacja { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteOgloszenieModel(ILogger<DeleteOgloszenieModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] OgloszenieClient client, [FromServices] LokalizacjaClient clientLokalizacja,
            [FromServices] UzytkownicyClient clientUzytkownik, int id)
        {
            Lokalizacja = await clientLokalizacja.GetLokalizacjaAsync();
            Uzytkownicy = await clientUzytkownik.GetUzytkownicyAsync();
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
        public async Task<IActionResult> OnPostAsync([FromServices] OgloszenieClient client, int id)
        {
            try
            {
                await client.DeleteOgloszenieAsync(id);
                return RedirectToPage("/Pelnomocnik/MojeOgloszenia");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
