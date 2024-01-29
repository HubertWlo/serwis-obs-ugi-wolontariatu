using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Lokalizacja
{
    public class DetailsLokalizacjaModel : PageModel
    {
        private readonly ILogger<DetailsLokalizacjaModel> _logger;
        public LokalizacjaInfo Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public DetailsLokalizacjaModel(ILogger<DetailsLokalizacjaModel> logger)
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
    }
}
