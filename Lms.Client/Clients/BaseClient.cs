using Lms.Common.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Clients
{
    public abstract class BaseClient
    {
        protected HttpClient HttpClient { get;  }
        public BaseClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri("https://localhost:7165");
        }

        public BaseClient(HttpClient httpClient, Uri uri) : this(httpClient) 
        {
            httpClient.BaseAddress = uri;
        }

        public async Task<T?> GetWithItAsync<T>(string path, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStreamAsync();
            //response.Dispose();

            return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
        }

    }
}
