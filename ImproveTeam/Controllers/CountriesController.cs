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
        public async Task<IActionResult> GetRegions(int countryId)
        {
            var regions = await _countryService.GetRegionsAsync(countryId);

            return Ok(_mapper.Map<IReadOnlyCollection<RegionViewModel>>(regions));
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var region = await _countryService.AddRegionAsync(model.CountryId, model.Name);

            return Ok(_mapper.Map<RegionViewModel>(region));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRegion(UpdateRegionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _countryService.UpdateRegionAsync(model.RegionId, model.Name);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRegion(int regionId)
        {
            await _countryService.DeleteRegionAsync(regionId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EditCountryPopup(int countryId)
        {
            var country = await _countryService.GetCountryByIdAsync(countryId);
            if(country == null)
            {
                return BadRequest($"Country #{countryId} not found");
            }

            return PartialView(_mapper.Map<UpdateCountryViewModel>(country));
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(int regionId)
        {
            var cities = await _countryService.GetCitiesAsync(regionId);

            return Ok(_mapper.Map<IReadOnlyCollection<CityViewModel>>(cities));
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(AddCityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = await _countryService.AddCityAsync(model.RegionId, model.Name);

            return Ok(_mapper.Map<CityViewModel>(city));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity(UpdateCityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _countryService.UpdateCityAsync(model.CityId, model.Name);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            await _countryService.DeleteCityAsync(cityId);

            return Ok();
        }
    }
}
