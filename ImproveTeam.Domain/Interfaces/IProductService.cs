using ImproveTeam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<Product>> GetProductsAsync();
        Task<Product> AddProductAsync(string name);
        Task UpdateProductAsync(int productId, string name);
        Task DeleteProductAsync(int productId);
    }
}
