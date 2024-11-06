﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rika_WebApp.Models;
using Rika_WebApp.ViewModels.Profile;
using System.Diagnostics;
using System.Text;

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
                userId = "1"; //ta bort denna sen
                var response = await _http.GetAsync($"http://localhost:7177/api/GetOneUserAsync?UserId={userId}");

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
					var userId = "1"; //ta bort denna sen, ska hämtas från cookie(?)
                    var updateModel = new UpdateUserModel
                    {
                        UserId = userId,
                        FirstName = detailsViewModel.FirstName,
                        LastName = detailsViewModel.LastName,
                        Email = detailsViewModel.Email,
                        Phonenumber = detailsViewModel.Phonenumber,
                        ProfileImageUrl = detailsViewModel.ProfileImageUrl,
                        Age = int.TryParse(detailsViewModel.Age, out int age) ? age : 0
                    };

					var json = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");
					var response = await _http.PutAsync("http://localhost:7177/api/UpdateUser", json);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details");
                    }
                }
				catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
			}
            return View(detailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            try
            {
                userId = "1";
                var response = await _http.DeleteAsync($"http://localhost:7177/api/DeleteUser?UserId={userId}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

            return RedirectToAction("Index");
        }
    }
}
