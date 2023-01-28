using AutoMapper;
using ImproveTeam.Domain.Constants;
using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Models.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Controllers
{
    [Authorize(Roles = CommonConstants.UserRole.TeamLead)]
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetCountriesAsync();
            var countryViewModels = _mapper.Map<IReadOnlyCollection<CountryViewModel>>(countries);

            return Ok(countryViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> EditCountryPopup(int countryId)
        {
            var country = await _countryService.GetCountryByIdAsync(countryId);
            if(country == null)
            {
                return BadRequest($"Country #{countryId} not found");
            }

            return PartialView(_mapper.Map<EditCountryViewModel>(country));
        }
    }
}
