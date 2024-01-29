using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Uzytkownik
{
    public class DetailsUzytkownikModel : PageModel
    {
        private readonly ILogger<DetailsUzytkownikModel> _logger;
        public UzytkownikInfo Uzytkownik { get; set; }

        public string ErrorMessage { get; set; }

        public DetailsUzytkownikModel(ILogger<DetailsUzytkownikModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] UzytkownicyClient client, int id)
        {
            try
            {
                Uzytkownik = await client.GetUzytkownikIdAsync(id);

                if (Uzytkownik == null)
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
