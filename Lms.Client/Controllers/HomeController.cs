using Lms.Client.Clients;
using Lms.Client.Models;
using Lms.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Controllers
{
    public class HomeController : Controller
    {

        private const string json = "application/json";
        //private readonly HttpClient httpClient;
        private readonly LmsClient httpClient;

        // public HomeController(IHttpClientFactory httpClientFactory)
        public HomeController(LmsClient lmsClient)
        {
            // httpClient = httpClientFactory.CreateClient("LmsClient");
            httpClient = lmsClient;
            //httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri("https://localhost:7165")
            //};

        }

        public async Task<IActionResult> Index()
        {
            var tournaments = await httpClient.GetWithItAsync();
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
