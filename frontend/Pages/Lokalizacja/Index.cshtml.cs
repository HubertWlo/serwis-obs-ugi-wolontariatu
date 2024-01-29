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
    public class IndexLokalizacjaModel : PageModel
    {
        private readonly ILogger<IndexLokalizacjaModel> _logger;
        public LokalizacjaInfo[] Lokalizacja { get; set; }

        public string ErrorMessage { get; set; }

        public IndexLokalizacjaModel(ILogger<IndexLokalizacjaModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] LokalizacjaClient client)
        {
            Lokalizacja = await client.GetLokalizacjaAsync();

            if (Lokalizacja.Count() == 0)
                ErrorMessage = "We must be sold out. Try again tomorrow.";
            else
                ErrorMessage = string.Empty;
        }
    }
}
