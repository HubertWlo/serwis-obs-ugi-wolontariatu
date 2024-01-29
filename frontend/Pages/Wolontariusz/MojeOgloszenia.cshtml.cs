﻿using System;
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
    public class MojeOgloszeniaWolontariuszModel : PageModel
    {
        private readonly ILogger<MojeOgloszeniaWolontariuszModel> _logger;
        public OgloszenieInfo[] Ogloszenia { get; set; }
        public LokalizacjaInfo[] Lokalizacje { get; set; }

        public string ErrorMessage { get; set; }

        public MojeOgloszeniaWolontariuszModel(ILogger<MojeOgloszeniaWolontariuszModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] OgloszenieClient clientOgloszenie, [FromServices] LokalizacjaClient clientLokalizacja)
        {
            Ogloszenia = await clientOgloszenie.GetOgloszeniaAsync();
            Lokalizacje = await clientLokalizacja.GetLokalizacjaAsync();
        }
    }
}
