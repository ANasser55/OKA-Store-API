using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OKA.Application.DTOs.Products
{
    public class CreateOrUpdateProductDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price can't be negative")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id value can't be negative")]
        public int CategoryId { get; set; }
    }
}
