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
    }
}
