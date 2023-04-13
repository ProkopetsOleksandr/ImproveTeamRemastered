using AutoMapper;
using ImproveTeam.Domain.Constants;
using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Controllers
{
    [Authorize(Roles = CommonConstants.UserRole.TeamLead)]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();

            return Ok(_mapper.Map<List<ProductViewModel>>(products));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await _productService.AddProductAsync(model.Name);

            return Ok(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productService.UpdateProductAsync(model.ProductId, model.Name);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);

            return Ok();
        }
    }
}
