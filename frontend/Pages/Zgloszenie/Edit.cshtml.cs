using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Zgloszenie
{
    public class EditZgloszenieModel : PageModel
    {
        private readonly ILogger<EditZgloszenieModel> _logger;
        public ZgloszenieInfo Zgloszenie { get; set; }

        public string ErrorMessage { get; set; }

        public EditZgloszenieModel(ILogger<EditZgloszenieModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] ZgloszenieClient client, int id)
        {
            try
            {
                Zgloszenie = await client.GetZgloszenieIdAsync(id);

                if (Zgloszenie == null)
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
                ZgloszenieInfo pom = new ZgloszenieInfo();
                pom = await client.GetZgloszenieIdAsync(id);
                pom.Tresc = tresc;
                await client.UpdateZgloszenieAsync(pom, id);

                return RedirectToPage("/Wolontariusz/MojeZgloszenia"); // Przekierowanie po zaktualizowaniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
