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
    public class AktywneOgloszeniaOgloszenieModel : PageModel
    {
        private readonly ILogger<AktywneOgloszeniaOgloszenieModel> _logger;
        public OgloszenieInfo[] Ogloszenia { get; set; }
        public LokalizacjaInfo[] Lokalizacje { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }
        public ZgloszenieInfo[] Zgloszenia { get; set; }
        public ZgloszenieInfo Zgloszenie { get; set; }
        public OgloszenieInfo Ogloszenie { get; set; }

        public string ErrorMessage { get; set; }

        public AktywneOgloszeniaOgloszenieModel(ILogger<AktywneOgloszeniaOgloszenieModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] OgloszenieClient clientOgloszenie, [FromServices] LokalizacjaClient clientLokalizacja, [FromServices] ZgloszenieClient clientZgloszenie)
        {
            Ogloszenia = await clientOgloszenie.GetOgloszeniaAsync();
            Lokalizacje = await clientLokalizacja.GetLokalizacjaAsync();
            Zgloszenia = await clientZgloszenie.GetOgloszeniaAsync();
            foreach (var o in Ogloszenia)
            {
                if(o.Data > DateTime.Now && o.OrganizatorId == o.WolontariuszId)
                {
                    Ogloszenie = o;
                    Zgloszenie = Zgloszenia.FirstOrDefault(z => z.OgloszenieId == Ogloszenie.Id);
                    if(Zgloszenie != null)
                    {
                        Ogloszenie.WolontariuszId = Zgloszenie.WolontariuszId;
                    }
                    else
                    {
                        Ogloszenie.WolontariuszId = 0;
                    }
                    await clientOgloszenie.UpdateOgloszenieAsync(Ogloszenie, Ogloszenie.Id);
                }
            }
        }
    }
}
