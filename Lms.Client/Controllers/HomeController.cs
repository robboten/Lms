using Lms.Client.Clients;
using Lms.Client.Models;
using Lms.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Controllers
{
    public class HomeController : Controller
    {

        private readonly LmsClient _lmsClient;
        public HomeController(LmsClient lmsClient)
        {
            _lmsClient = lmsClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _lmsClient.GetWithItAsync<IEnumerable<TournamentDto>>("api/Tournaments"));
            //return View(await _lmsClient.GetAllAsync());

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
