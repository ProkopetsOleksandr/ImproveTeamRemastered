using ImproveTeam.Domain.Constants;
using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Models.Interests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImproveTeam.Controllers
{
    [Authorize(Roles = CommonConstants.UserRole.TeamLead)]
    public class InterestsController : Controller
    {
        private readonly IInterestsService _interestsService;

        public InterestsController(IInterestsService interestsService)
        {
            _interestsService = interestsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetInterests()
        {
            var interests = await _interestsService.GetInterestsAsync();

            return Ok(interests);
        }

        [HttpPost]
        public async Task<IActionResult> AddInterest(AddInterestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await _interestsService.AddInterestAsync(model.Name);

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInterest(UpdateInterestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _interestsService.UpdateInterestAsync(model.InterestId, model.Name);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInterest(int interestId)
        {
            await _interestsService.DeleteInterestAsync(interestId);

            return Ok();
        }
    }
}
