using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ProductManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public ProductRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.IsActive);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedDate = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            product.ModifiedDate = DateTime.UtcNow;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsActive = false;
                product.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _context.Products
                .Where(p => p.Category == category && p.IsActive)
                .ToListAsync();
        }

        // Advanced SQL: Using CTE and PIVOT
        public async Task<object> GetProductSalesSummaryAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"
                WITH SalesCTE AS (
                    SELECT 
                        Category,
                        DATEPART(MONTH, CreatedDate) as SalesMonth,
                        COUNT(*) as ProductCount,
                        SUM(Price * StockQuantity) as TotalValue
                    FROM Products
                    WHERE IsActive = 1
                    GROUP BY Category, DATEPART(MONTH, CreatedDate)
                )
                
                SELECT *
                FROM (
                    SELECT 
                        Category,
                        'Month_' + CAST(SalesMonth as VARCHAR(2)) as MonthColumn,
                        TotalValue
                    FROM SalesCTE
                ) AS SourceTable
                PIVOT(
                    SUM(TotalValue)
                    FOR MonthColumn IN ([Month_1], [Month_2], [Month_3], [Month_4], [Month_5], [Month_6],
                                       [Month_7], [Month_8], [Month_9], [Month_10], [Month_11], [Month_12])
                ) AS PivotTable";

            return await connection.QueryAsync<dynamic>(sql);
        }

        // JSON Operations: Update specifications
        public async Task<string> UpdateProductSpecificationsAsync(int productId, Dictionary<string, object> specifications)
        {
            var jsonSpecs = System.Text.Json.JsonSerializer.Serialize(specifications);

            using var connection = new SqlConnection(_connectionString);

            var sql = @"
                UPDATE Products 
                SET Specifications = @JsonSpecs, ModifiedDate = GETUTCDATE()
                WHERE ProductId = @ProductId
                
                SELECT Specifications FROM Products WHERE ProductId = @ProductId";

            var result = await connection.QueryFirstOrDefaultAsync<string>(sql, new
            {
                JsonSpecs = jsonSpecs,
                ProductId = productId
            });

            return result;
        }

        // JSON Operations: Get specifications using JSON_VALUE and JSON_QUERY
        public async Task<Dictionary<string, object>> GetProductSpecificationsAsync(int productId)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"
                SELECT 
                    JSON_VALUE(Specifications, '$.color') as Color,
                    JSON_VALUE(Specifications, '$.weight') as Weight,
                    JSON_VALUE(Specifications, '$.dimensions.length') as Length,
                    JSON_QUERY(Specifications, '$.features') as Features,
                    Specifications as FullSpecifications
                FROM Products 
                WHERE ProductId = @ProductId AND IsActive = 1";

            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { ProductId = productId });

            if (result?.FullSpecifications != null)
            {
                return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(result.FullSpecifications);
            }

            return new Dictionary<string, object>();
        }
    }
}