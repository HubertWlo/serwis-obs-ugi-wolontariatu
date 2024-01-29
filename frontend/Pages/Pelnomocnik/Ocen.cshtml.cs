using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class OcenPelnomocnikModel : PageModel
    {
        private readonly ILogger<OcenPelnomocnikModel> _logger;
        public OgloszenieInfo Ogloszenie { get; set; }
        public ZgloszenieInfo Zgloszenie { get; set; }
        public ZgloszenieInfo[] Zgloszenia { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync([FromServices] OgloszenieClient client, [FromServices] LokalizacjaClient clientLokalizacja, int id)
        {
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
        public async Task<IActionResult> OnPostAsync([FromServices] OgloszenieClient client, [FromServices] ZgloszenieClient zgloszenieClient, int id, int ocena)
        {
            try
            {
                Ogloszenie = await client.GetOgloszenieIdAsync(id);
                Zgloszenia = await zgloszenieClient.GetOgloszeniaAsync();
                Zgloszenie = Zgloszenia.FirstOrDefault(z => z.WolontariuszId == Ogloszenie.WolontariuszId && z.OgloszenieId == Ogloszenie.Id);
                Zgloszenie.Ocena = ocena;

                await zgloszenieClient.UpdateZgloszenieAsync(Zgloszenie, Zgloszenie.Id);

                if (Request.Cookies.TryGetValue("UserRole", out string rola) && rola == "Opiekun")
                {
                    return RedirectToPage("/Pelnomocnik/MojeOgloszenia");
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
