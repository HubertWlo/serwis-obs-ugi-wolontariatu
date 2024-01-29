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
    public class CreateLokalizacjaModel : PageModel
    {
        private readonly ILogger<CreateLokalizacjaModel> _logger;
        [BindProperty]
        public LokalizacjaInfo Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public CreateLokalizacjaModel(ILogger<CreateLokalizacjaModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Akcje wykonywane przy pobieraniu strony Create.cshtml
        }

        public async Task<IActionResult> OnPostAsync([FromServices] LokalizacjaClient client)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Request.Cookies.TryGetValue("UserId", out string id);
                Lokalizacja.WlascicielId = id;
                await client.CreateLokalizacjaAsync(Lokalizacja);
                return RedirectToPage("/Ogloszenie/Create"); // Przekierowanie po pomyœlnym utworzeniu og³oszenia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
