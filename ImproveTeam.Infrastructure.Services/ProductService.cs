using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContext _dbContext;

        public ProductService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            return await _dbContext.Set<Product>().ToListAsync();
        }

        public async Task<Product> AddProductAsync(string name)
        {
            var product = new Product
            {
                Name = name
            };

            _dbContext.Set<Product>().Add(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task UpdateProductAsync(int productId, string name)
        {
            var product = await _dbContext.Set<Product>().FindAsync(productId);
            if (product == null)
            {
                return;
            }

            product.Name = name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = new Product { Id = productId };

            _dbContext.Set<Product>().Attach(product);
            _dbContext.Set<Product>().Remove(product);

            await _dbContext.SaveChangesAsync();
        }
    }
}
