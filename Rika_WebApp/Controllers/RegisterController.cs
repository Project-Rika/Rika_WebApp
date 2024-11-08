using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rika_WebApp.ViewModels;
using Rika_WebApp.Configurations;


namespace Rika_WebApp.Controllers;

public class RegisterController(IHttpClientFactory httpClientFactory, IOptions<ApiUris> options) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly ApiUris _apiUris = options.Value;

    // GET: /Register
    public IActionResult Index()
    {
        return View();
    }

    // POST: /Register
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Return the view with validation messages if the model is invalid
            return View("Index", model);
        }

        try
        {
            // Send the registration data to the CreateUser API
            var response = await _httpClient.PostAsJsonAsync($"{_apiUris.BaseUrl}{_apiUris.CreateUser}?code={_apiUris.ApiKey}", model);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to a success page or login page after successful registration
                return RedirectToAction("Index", "Home");
            }

            // Add an error to the ModelState if the API call failed
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to create user. Error: {errorMessage}");
        }
        catch (Exception ex)
        {
            // Handle unexpected exceptions
            ModelState.AddModelError("", $"An unexpected error occurred: {ex.Message}");
        }

        // Return the view with validation messages if API call failed
        return View("Index", model);
    }
}




