using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class CreateZgloszenieModel : PageModel
    {
        private readonly ILogger<CreateZgloszenieModel> _logger;
        [BindProperty]
        public ZgloszenieInfo Zgloszenie { get; set; }

        public string ErrorMessage { get; set; }

        public CreateZgloszenieModel(ILogger<CreateZgloszenieModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Akcje wykonywane przy pobieraniu strony Create.cshtml
        }

        public async Task<IActionResult> OnPostAsync([FromServices] ZgloszenieClient client)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await client.CreateZgloszenieAsync(Zgloszenie);
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
