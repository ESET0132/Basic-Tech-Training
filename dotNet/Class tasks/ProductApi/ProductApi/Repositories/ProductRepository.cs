using ProductApi.Models;

namespace ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        private int _nextId = 1;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = _nextId++, Name = "Laptop", Description = "Gaming Laptop", Price = 999.99m, Stock = 10 },
                new Product { Id = _nextId++, Name = "Mouse", Description = "Wireless Mouse", Price = 29.99m, Stock = 50 },
                new Product { Id = _nextId++, Name = "Keyboard", Description = "Mechanical Keyboard", Price = 79.99m, Stock = 25 }
            };
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<Product> CreateAsync(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return Task.FromResult(product);
        }

        public Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                return Task.FromResult(existingProduct)!;
            }
            return Task.FromResult<Product?>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}