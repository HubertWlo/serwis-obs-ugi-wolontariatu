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
    public class MojeZgloszeniaWolontariuszModel : PageModel
    {
        private readonly ILogger<MojeZgloszeniaWolontariuszModel> _logger;
        public ZgloszenieInfo[] Zgloszenia { get; set; }
        public OgloszenieInfo[] Ogloszenia { get; set; }

        public string ErrorMessage { get; set; }

        public MojeZgloszeniaWolontariuszModel(ILogger<MojeZgloszeniaWolontariuszModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] OgloszenieClient clientOgloszenie, [FromServices] ZgloszenieClient clientZgloszenie)
        {
            if (Request.Cookies.TryGetValue("UserId", out string id))
            {
                int wolontariuszId = int.Parse(id);
                Zgloszenia = await clientZgloszenie.GetZgloszenieWolontariuszIdAsync(wolontariuszId);
            }
            Ogloszenia = await clientOgloszenie.GetOgloszeniaAsync();
        }
    }
}
