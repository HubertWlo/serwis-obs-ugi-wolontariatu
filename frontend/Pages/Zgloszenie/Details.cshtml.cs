using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using frontend.Controllers;

namespace frontend.Pages.Zgloszenie
{
    public class DetailsZgloszenieModel : PageModel
    {
        private readonly ILogger<DetailsZgloszenieModel> _logger;
        public ZgloszenieInfo Zgloszenie { get; set; }

        public string ErrorMessage { get; set; }

        public DetailsZgloszenieModel(ILogger<DetailsZgloszenieModel> logger)
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
    }
}
