using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using frontend.Controllers;
using frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace frontend.Pages
{
    public class LoginUzytkownikModel : PageModel
    {
        private readonly ILogger<LoginUzytkownikModel> _logger;
        [BindProperty]
        public UzytkownikInfo Uzytkownik { get; set; }
        public UzytkownikInfo[] Uzytkownicy { get; set; }

        public string ErrorMessage { get; set; }

        public LoginUzytkownikModel(ILogger<LoginUzytkownikModel> logger)
        {
            _logger = logger;
        }
        public async Task OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync([FromServices] UzytkownicyClient client)
        {
            Uzytkownicy = await client.GetUzytkownicyAsync();
            try
            {
                foreach(var item in Uzytkownicy)
                {
                    if (item.Login == Uzytkownik.Login && item.Haslo == Uzytkownik.Haslo)
                    {
                        Response.Cookies.Append("UserId", item.Id.ToString());
                        Response.Cookies.Append("UserRole", item.Rola);
                        Response.Cookies.Append("UserLogin", item.Login);
                        return RedirectToPage("../Index");
                    }
                } 
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            TempData["BladLogowania"] = "Nieudana próba logowania. SprawdŸ poprawnoœæ danych.";
            return RedirectToPage("./Login");
        }
    }
}
