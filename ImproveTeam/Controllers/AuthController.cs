using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImproveTeam.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("/Home/Index");
            }

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.VerifyUserCredentialsAsync(model.Login, model.Password);
            if (!result.IsUserValid)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");

                return View(model);
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                GetClaimsPrincipal(result.User),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(30),
                    IsPersistent = true,
                    AllowRefresh = true
                });

            return !string.IsNullOrEmpty(model.ReturnUrl)
                ? Redirect(model.ReturnUrl)
                : Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        private ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Login"));
        }
    }
}
