using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace frontend.Controllers
{
    public class OgloszenieClient
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        private readonly ILogger<OgloszenieClient> _logger;

        public OgloszenieClient(HttpClient client, ILogger<OgloszenieClient> logger)
        {
            this.client = client;
            _logger = logger;
        }

        public async Task<OgloszenieInfo[]> GetOgloszeniaAsync()
        {
            try
            {
                var responseMessage = await client.GetAsync("/api/Items");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    var ogloszenia = await JsonSerializer.DeserializeAsync<OgloszenieInfo[]>(stream, options);
                    return ogloszenia;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new OgloszenieInfo[] { };

        }

        public async Task<OgloszenieInfo> GetOgloszenieIdAsync(int id)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/Items/+{id}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<OgloszenieInfo>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new OgloszenieInfo { };

        }

        public async Task<OgloszenieInfo[]> GetOgloszenieWolontariuszIdAsync(int wolontariuszId)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/ItemsWolontariusz/+{wolontariuszId}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    var ogloszenia = await JsonSerializer.DeserializeAsync<OgloszenieInfo[]>(stream, options);
                    return ogloszenia;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new OgloszenieInfo[] { };

        }
        public async Task<OgloszenieInfo[]> GetOgloszenieOpiekunIdAsync(int opiekunId)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/ItemsOpiekun/+{opiekunId}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    var ogloszenia = await JsonSerializer.DeserializeAsync<OgloszenieInfo[]>(stream, options);
                    return ogloszenia;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new OgloszenieInfo[] { };

        }

        public async Task CreateOgloszenieAsync(OgloszenieInfo ogloszenie)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(ogloszenie);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("/api/Items", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteOgloszenieAsync(int id)
        {
            try
            {
                var responseMessage = await client.DeleteAsync($"/api/Items/+{id}");

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateOgloszenieAsync(OgloszenieInfo ogloszenie, int id)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(ogloszenie);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PutAsync($"/api/Items/+{id}", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public async Task<ModelListy<OgloszenieInfo>> GetOgloszeniaInListAsync()
        {
            try
            {
                var responseMessage = await client.GetAsync("/api/Uzytkownik");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    var ogloszenia = await JsonSerializer.DeserializeAsync <ModelListy<OgloszenieInfo>> (stream, options);
                    return ogloszenia;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new ModelListy<OgloszenieInfo> { };

        }
    }
}