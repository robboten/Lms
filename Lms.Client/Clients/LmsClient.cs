using Lms.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Clients
{
    public class LmsClient
    {
        private readonly HttpClient _httpClient;
        public LmsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7165");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<TournamentDto?>> GetWithItAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Tournaments");
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var tournaments = JsonSerializer.Deserialize<IEnumerable<TournamentDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return tournaments!;
        }
    }
}
