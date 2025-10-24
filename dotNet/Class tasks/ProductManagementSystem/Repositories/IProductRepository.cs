using ProductManagementSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagementSystem.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
        Task<object> GetProductSalesSummaryAsync();
        Task<string> UpdateProductSpecificationsAsync(int productId, Dictionary<string, object> specifications);
        Task<Dictionary<string, object>> GetProductSpecificationsAsync(int productId);
    }
}