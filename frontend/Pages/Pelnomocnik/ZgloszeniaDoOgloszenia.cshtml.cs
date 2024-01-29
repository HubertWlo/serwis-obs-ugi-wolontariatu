using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages.Pelnomocnik
{
    public class ZgloszeniaDoOgloszeniaModel : PageModel
    {
        private readonly ILogger<ZgloszeniaDoOgloszeniaModel> _logger;
        public ZgloszenieInfo[] Zgloszenia { get; set; }
        public ZgloszenieInfo Zgloszenie { get; set; }
        public OgloszenieInfo Ogloszenie { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }

        public string ErrorMessage { get; set; }

        public ZgloszeniaDoOgloszeniaModel(ILogger<ZgloszeniaDoOgloszeniaModel> logger)
        {
            _logger = logger;
        }
        public async Task OnGetAsync([FromServices] ZgloszenieClient client, [FromServices] UzytkownicyClient clientUzytkownik, int id)
        {
            Zgloszenia = await client.GetZgloszenieOgloszenieIdAsync(id);
            Uzytkownicy = await clientUzytkownik.GetUzytkownicyAsync();
        }
        public async Task<IActionResult> OnPostAsync([FromServices] ZgloszenieClient client, [FromServices] OgloszenieClient OglClient, int id)
        {
            try
            {
                Zgloszenie = await client.GetZgloszenieIdAsync(id);
                OgloszenieInfo Ogloszenie = new OgloszenieInfo();
                Ogloszenie = await OglClient.GetOgloszenieIdAsync(Zgloszenie.OgloszenieId);
                Ogloszenie.WolontariuszId = Zgloszenie.WolontariuszId;
                await OglClient.UpdateOgloszenieAsync(Ogloszenie, Ogloszenie.Id);
                return RedirectToPage("./MojeOgloszenia"); // Przekierowanie po pomyœlnym utworzeniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
        }
    }
}
