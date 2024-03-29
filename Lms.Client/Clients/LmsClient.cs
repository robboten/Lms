﻿using System.Net.Http.Headers;
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
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T?> GetWithItAsync<T>(string path, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStreamAsync();
            //response.Dispose();

            return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
        }

    }
}
