using Microsoft.AspNetCore.Mvc;

namespace Rika_WebApp.Controllers;

public class RegisterController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
