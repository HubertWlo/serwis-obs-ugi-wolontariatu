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
    public class LokalizacjaClient
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        private readonly ILogger<LokalizacjaClient> _logger;

        public LokalizacjaClient(HttpClient client, ILogger<LokalizacjaClient> logger)
        {
            this.client = client;
            _logger = logger;
        }

        public async Task<LokalizacjaInfo[]> GetLokalizacjaAsync()
        {
            try
            {
                var responseMessage = await client.GetAsync("/api/Lokalizacja");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<LokalizacjaInfo[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new LokalizacjaInfo[] { };

        }

        public async Task<LokalizacjaInfo> GetLokalizacjaIdAsync(int id)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/Lokalizacja/+{id}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<LokalizacjaInfo>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new LokalizacjaInfo { };

        }

        public async Task CreateLokalizacjaAsync(LokalizacjaInfo lokalizacja)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(lokalizacja);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("/api/Lokalizacja", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteLokalizacjaAsync(int id)
        {
            try
            {
                var responseMessage = await client.DeleteAsync($"/api/Lokalizacja/+{id}");

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateLokalizacjaAsync(LokalizacjaInfo lokalizacja, int id)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(lokalizacja);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PutAsync($"/api/Lokalizacja/+{id}", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}