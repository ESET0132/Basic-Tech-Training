using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Category { get; set; }

        public string Specifications { get; set; }

        public Dictionary<string, object> Specs { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Category { get; set; }

        public Dictionary<string, object> Specifications { get; set; }
    }
}