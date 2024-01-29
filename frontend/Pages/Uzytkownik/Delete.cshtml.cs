using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Uzytkownik
{
    public class DeleteUzytkownikModel : PageModel
    {
        private readonly ILogger<DeleteUzytkownikModel> _logger;
        public UzytkownikInfo Uzytkownik { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteUzytkownikModel(ILogger<DeleteUzytkownikModel> logger)
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
        public async Task<IActionResult> OnPostAsync([FromServices] UzytkownicyClient client, int id)
        {
            try
            {
                await client.DeleteUzytkownikAsync(id);
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
