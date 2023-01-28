using ImproveTeam.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImproveTeam.Controllers
{
    [Authorize(Roles = CommonConstants.UserRole.TeamLead)]
    public class CountriesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
