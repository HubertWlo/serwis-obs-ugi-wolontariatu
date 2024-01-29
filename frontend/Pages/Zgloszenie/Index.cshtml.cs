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
    public class IndexZgloszenieModel : PageModel
    {
        private readonly ILogger<IndexZgloszenieModel> _logger;
        public ZgloszenieInfo[] Zgloszenia { get; set; }

        public string ErrorMessage { get; set; }

        public IndexZgloszenieModel(ILogger<IndexZgloszenieModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] ZgloszenieClient client)
        {
            Zgloszenia = await client.GetOgloszeniaAsync();
        }
    }
}
