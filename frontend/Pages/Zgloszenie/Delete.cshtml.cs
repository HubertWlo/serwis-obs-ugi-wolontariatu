using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Zgloszenie
{
    public class DeleteZgloszenieModel : PageModel
    {
        private readonly ILogger<DeleteZgloszenieModel> _logger;
        public ZgloszenieInfo Zgloszenie { get; set; }
        public OgloszenieInfo Ogloszenie { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteZgloszenieModel(ILogger<DeleteZgloszenieModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync([FromServices] ZgloszenieClient client, [FromServices] OgloszenieClient clientOgloszenie, int id)
        {
            try
            {
                Zgloszenie = await client.GetZgloszenieIdAsync(id);
                Ogloszenie = await clientOgloszenie.GetOgloszenieIdAsync(Zgloszenie.OgloszenieId);
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
        public async Task<IActionResult> OnPostAsync([FromServices] ZgloszenieClient client, int id)
        {
            try
            {
                await client.DeleteZgloszenieAsync(id);
                return RedirectToPage("/Wolontariusz/MojeZgloszenia"); // Przekierowanie po pomyœlnym utworzeniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
