using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Models.Advertiser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImproveTeam.Controllers
{
    public class AdvertisersController : Controller
    {
        private readonly IAdvertiserService _advertiserService;
        private readonly IProductService _productService;

        public AdvertisersController(IAdvertiserService advertiserService, IProductService productService)
        {
            _advertiserService = advertiserService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisers()
        {
            var advertisers = await _advertiserService.GetAdvertisersInfoAsync();

            return Ok(advertisers);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvertiser(AddAdvertiserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _advertiserService.AddAdvertiserAsync(model.Name, model.ProductIds);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdvertiser(UpdateAdvertiserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _advertiserService.UpdateAdvertiserAsync(model.Id, model.Name, model.ProductIds);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdvertiser(int advertiserId)
        {
            await _advertiserService.DeleteAdvertiserAsync(advertiserId);

            return Ok();
        }
    }
}
