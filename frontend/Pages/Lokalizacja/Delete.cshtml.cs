using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Lokalizacja
{
    public class DeleteLokalizacjaModel : PageModel
    {
        private readonly ILogger<DeleteLokalizacjaModel> _logger;
        public LokalizacjaInfo Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteLokalizacjaModel(ILogger<DeleteLokalizacjaModel> logger)
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
        public async Task<IActionResult> OnPostAsync([FromServices] LokalizacjaClient client, int id)
        {
            try
            {
                await client.DeleteLokalizacjaAsync(id);
                return RedirectToPage("./Index"); // Przekierowanie po pomyœlnym utworzeniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
