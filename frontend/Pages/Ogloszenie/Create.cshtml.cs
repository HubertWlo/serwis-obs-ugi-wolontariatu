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
    public class CreateOgloszenieModel : PageModel
    {
        private readonly ILogger<CreateOgloszenieModel> _logger;
        [BindProperty]
        public OgloszenieInfo Ogloszenie { get; set; }
        public LokalizacjaInfo[] Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public CreateOgloszenieModel(ILogger<CreateOgloszenieModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] LokalizacjaClient clientLokalizacja)
        {
            Lokalizacja = await clientLokalizacja.GetLokalizacjaAsync();
            Request.Cookies.TryGetValue("UserId", out string id);
            var LokalizajcaUz = Lokalizacja.Where(l => l.WlascicielId == id);
            ViewData["LokalizacjaId"] = new SelectList(LokalizajcaUz, "Id", "Nazwa");
            // Akcje wykonywane przy pobieraniu strony Create.cshtml
        }

        public async Task<IActionResult> OnPostAsync([FromServices] OgloszenieClient client)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (Request.Cookies.TryGetValue("UserId", out string id))
                {
                    int opiekunId = int.Parse(id);
                    Ogloszenie.WolontariuszId = opiekunId;
                    Ogloszenie.OrganizatorId = opiekunId;
                }
                await client.CreateOgloszenieAsync(Ogloszenie);
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
