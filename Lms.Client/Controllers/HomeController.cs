using Lms.Client.Models;
using Lms.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient;
        private const string json = "application/json";

        public HomeController(ILogger<HomeController> logger)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7165")
            };
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var response = await httpClient.GetAsync("api/Tournaments/?IncludeGames=false");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/Tournaments");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var tournaments = JsonSerializer.Deserialize<IEnumerable<TournamentDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return View(tournaments);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}