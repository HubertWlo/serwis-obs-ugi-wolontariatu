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
    public class ZgloszenieClient
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        private readonly ILogger<ZgloszenieClient> _logger;

        public ZgloszenieClient(HttpClient client, ILogger<ZgloszenieClient> logger)
        {
            this.client = client;
            _logger = logger;
        }

        public async Task<ZgloszenieInfo[]> GetOgloszeniaAsync()
        {
            try
            {
                var responseMessage = await client.GetAsync("/api/Zgloszenie");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ZgloszenieInfo[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new ZgloszenieInfo[] { };

        }

        public async Task<ZgloszenieInfo> GetZgloszenieIdAsync(int id)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/Zgloszenie/+{id}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ZgloszenieInfo>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new ZgloszenieInfo { };

        }
        public async Task<ZgloszenieInfo[]> GetZgloszenieWolontariuszIdAsync(int wolontariuszId)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/ZgloszenieWolontariusz/+{wolontariuszId}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ZgloszenieInfo[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new ZgloszenieInfo[] { };

        }
        public async Task<ZgloszenieInfo[]> GetZgloszenieOgloszenieIdAsync(int ogloszenieId)
        {
            try
            {
                var responseMessage = await client.GetAsync($"/api/ZgloszenieOpiekun/+{ogloszenieId}");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ZgloszenieInfo[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new ZgloszenieInfo[] { };

        }

        public async Task CreateZgloszenieAsync(ZgloszenieInfo zgloszenie)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(zgloszenie);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("/api/Zgloszenie", content);

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteZgloszenieAsync(int id)
        {
            try
            {
                var responseMessage = await client.DeleteAsync($"/api/Zgloszenie/+{id}");

                responseMessage.EnsureSuccessStatusCode(); // Upewnienie siê, ¿e ¿¹danie by³o udane (status 200-299)
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateZgloszenieAsync(ZgloszenieInfo zgloszenie, int id)
        {
            try
            {
                // Serializacja og³oszenia do formatu JSON
                var json = JsonSerializer.Serialize(zgloszenie);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PutAsync($"/api/Zgloszenie/+{id}", content);

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