using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class EditPersonalUzytkownikModel : PageModel
    {
        private readonly ILogger<EditPersonalUzytkownikModel> _logger;
        [BindProperty]
        public UzytkownikInfo Uzytkownik { get; set; }

        public string ErrorMessage { get; set; }

        public EditPersonalUzytkownikModel(ILogger<EditPersonalUzytkownikModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromServices] UzytkownicyClient client)
        {
            Request.Cookies.TryGetValue("UserId", out string Id);
            int id = int.Parse(Id);
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

        public void TaskOnPost()
        {
        }
    }
}
