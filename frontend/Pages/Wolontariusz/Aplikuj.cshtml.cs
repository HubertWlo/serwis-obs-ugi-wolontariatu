using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Wolontariusz
{
    public class AplikujWolontariuszModel : PageModel
    {
        private readonly ILogger<AplikujWolontariuszModel> _logger;
        public OgloszenieInfo Ogloszenie { get; set; }
        public ZgloszenieInfo Zgloszenie { get; set; }
        public LokalizacjaInfo[] Lokalizacja { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }


        public string ErrorMessage { get; set; }

        public AplikujWolontariuszModel(ILogger<AplikujWolontariuszModel> logger)
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
        public async Task<IActionResult> OnPostAsync([FromServices] ZgloszenieClient client, int id, String tresc)
        {
            try
            {
                //Ogloszenie = await client.GetOgloszenieIdAsync(id);
                ZgloszenieInfo Zgloszenie = new ZgloszenieInfo();
                Zgloszenie.OgloszenieId = id;
                Zgloszenie.Ocena = 0;
                Zgloszenie.Tresc = tresc;
                if (Request.Cookies.TryGetValue("UserId", out string woloId))
                {
                    int wolontariuszId = int.Parse(woloId);
                    Zgloszenie.WolontariuszId = wolontariuszId;
                }
                await client.CreateZgloszenieAsync(Zgloszenie);
                return RedirectToPage("/Wolontariusz/MojeZgloszenia"); // Przekierowanie po pomyœlnym utworzeniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
