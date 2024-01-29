using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontend.Pages.Ogloszenie
{
    public class EditOgloszenieModel : PageModel
    {
        private readonly ILogger<EditOgloszenieModel> _logger;
        public OgloszenieInfo Ogloszenie { get; set; }
        public LokalizacjaInfo[] Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public EditOgloszenieModel(ILogger<EditOgloszenieModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] OgloszenieClient client, [FromServices] LokalizacjaClient clientLokalizacja, int id)
        {
            try
            {
                Lokalizacja = await clientLokalizacja.GetLokalizacjaAsync();
                ViewData["LokalizacjaId"] = new SelectList(Lokalizacja, "Id", "Nazwa");
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
        public async Task<IActionResult> OnPostAsync([FromServices] OgloszenieClient client, int id, string nazwa, string opis, DateTime data, int lokalizacja)
        {
            try
            {
                Ogloszenie = await client.GetOgloszenieIdAsync(id);
                OgloszenieInfo pom = new OgloszenieInfo();
                pom.Id = id;
                pom.Nazwa = nazwa;
                pom.Opis = opis;
                pom.WolontariuszId = Ogloszenie.WolontariuszId;
                pom.OrganizatorId = Ogloszenie.OrganizatorId;
                pom.Data = data;
                pom.LokalizacjaId = lokalizacja;
                await client.UpdateOgloszenieAsync(pom, id);

                if (Request.Cookies.TryGetValue("UserRole", out string rola))
                {
                    if(rola == "Pracownik")
                    {
                        return RedirectToPage("./AktywneOgloszenia"); 
                    }
                    else
                    {
                        return RedirectToPage("/Pelnomocnik/MojeOgloszenia");
                    }
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
