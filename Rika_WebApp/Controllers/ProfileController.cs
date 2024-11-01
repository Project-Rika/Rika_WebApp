using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rika_WebApp.ViewModels.Profile;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace Rika_WebApp.Controllers
{
    public class ProfileController(HttpClient http) : Controller
    {
        private readonly HttpClient _http = http;

        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/profile/details")]
        public async Task<IActionResult> Details(string userId)
        {
            var viewModel = new ProfileFormViewModel();
            try
            {
                userId = "u1"; //ta bort denna sen
                var response = await _http.GetAsync($"localhost.se/api/GetOneUserAsync?Id={userId}");

                if (response.IsSuccessStatusCode)
                {
                    viewModel = JsonConvert.DeserializeObject<ProfileFormViewModel>(await response.Content.ReadAsStringAsync());

                    return View(viewModel);
                }
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Details()
        {
            //göra denna sen på update branchen?
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}
