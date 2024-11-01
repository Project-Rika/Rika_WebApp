using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rika_WebApp.Models;
using Rika_WebApp.ViewModels.Profile;
using System.Diagnostics;
using System.Text;
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
        public async Task<IActionResult> Details(ProfileFormViewModel detailsViewModel)
        {
            if (ModelState.IsValid)
            {
				try
				{
					var userId = "u1"; //ta bort denna sen, ska hämtas från cookie(?)
                    var updateModel = new UpdateUserModel
                    {
                        UserId = userId,
                        FirstName = detailsViewModel.FirstName,
                        LastName = detailsViewModel.LastName,
                        Email = detailsViewModel.Email,
                        Password = detailsViewModel.Password,
                        Phonenumber = detailsViewModel.Phonenumber,
                        ProfileImageUrl = detailsViewModel.ProfileImageUrl,
                        Age = detailsViewModel.Age,
                        Gender = detailsViewModel.Gender
                    };

					var json = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");
					var response = await _http.PostAsync("https://localhost.se/api/UpdateUser", json);

					if (response.IsSuccessStatusCode)
					{
						var userData = JsonConvert.DeserializeObject<UpdateUserModel>(await response.Content.ReadAsStringAsync());

                        if (userData != null)
                        {
							detailsViewModel.FirstName = userData.FirstName;
							detailsViewModel.LastName = userData.LastName;
							detailsViewModel.Email = userData.Email;
							detailsViewModel.Password = userData.Password;
							detailsViewModel.Phonenumber = userData.Phonenumber;
							detailsViewModel.ProfileImageUrl = userData.ProfileImageUrl;
							detailsViewModel.Age = userData.Age;
							detailsViewModel.Gender = userData.Gender;

							return View(detailsViewModel);
						}
					}
				}
				catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
			}
            return View(detailsViewModel);
        }
    }
}
