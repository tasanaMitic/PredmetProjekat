using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public  Brand Brand { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public bool IsInStock { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        [Precision(18,2)]
        public decimal Price { get; set; }
        [Required]
        public ProductType ProductType { get; set; }

    }
}
