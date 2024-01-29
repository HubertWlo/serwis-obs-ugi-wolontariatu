using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Lokalizacja
{
    public class EditLokalizacjaModel : PageModel
    {
        private readonly ILogger<EditLokalizacjaModel> _logger;
        public LokalizacjaInfo Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public EditLokalizacjaModel(ILogger<EditLokalizacjaModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] LokalizacjaClient client, int id)
        {
            try
            {
                Lokalizacja = await client.GetLokalizacjaIdAsync(id);

                if (Lokalizacja == null)
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
        public async Task<IActionResult> OnPostAsync([FromServices] LokalizacjaClient client, int id, string nazwa, string miasto, string szerokosc, string dlugosc)
        {
            try
            {
                LokalizacjaInfo pom = new LokalizacjaInfo();
                pom.Id = id;
                pom.Nazwa = nazwa;
                pom.Miasto = miasto;
                pom.DlugoscGeo = dlugosc;
                pom.SzerokoscGeo = szerokosc;
                await client.UpdateLokalizacjaAsync(pom, id);

                return RedirectToPage("./Index"); // Przekierowanie po zaktualizowaniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
