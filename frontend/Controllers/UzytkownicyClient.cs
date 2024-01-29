using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace frontend.Controllers
{
    public class UzytkownicyClient
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        private readonly ILogger<UzytkownicyClient> _logger;

        public UzytkownicyClient(HttpClient client, ILogger<UzytkownicyClient> logger)
        {
            this.client = client;
            _logger = logger;
        }

        public async Task<UzytkownikInfo[]> GetUzytkownicyAsync()
        {
            try
            {
                var responseMessage = await client.GetAsync("/api/Uzytkownik");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<UzytkownikInfo[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new UzytkownikInfo[] { };

        }

        public async Task<UzytkownikInfo> GetUzytkownikIdAsync(int id)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/Uzytkownik/+{id}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<UzytkownikInfo>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new UzytkownikInfo { };

        }

        public async Task CreateUzytkownikAsync(UzytkownikInfo uzytkownik)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(uzytkownik);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("/api/Uzytkownik", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteUzytkownikAsync(int id)
        {
            try
            {
                var responseMessage = await client.DeleteAsync($"/api/Uzytkownik/+{id}");

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateUzytkownikAsync(UzytkownikInfo uzytkownik, int id)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(uzytkownik);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PutAsync($"/api/Uzytkownik/+{id}", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public async Task<IActionResult> Login(string login, string haslo, HttpContext httpContext)
        {
            try
            {
                var responseMessage = await client.GetAsync("/api/Uzytkownik");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    UzytkownikInfo[] Uzytkownicy = await JsonSerializer.DeserializeAsync<UzytkownikInfo[]>(stream, options);
                    foreach(UzytkownikInfo uzytkownik in Uzytkownicy)
                    {
                        if(uzytkownik.Login == login && uzytkownik.Haslo == haslo)
                        {
                            httpContext.Items["User"] = uzytkownik;
                            return new RedirectToPageResult("./Index");
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new RedirectToPageResult("./Login");
        }
    }
}