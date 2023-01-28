using ImproveTeam.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImproveTeam.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }
    }
}
