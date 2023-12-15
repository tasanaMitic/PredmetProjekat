using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class SoldProduct
    {
        [Key]
        public Guid SoldProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        [Range(1, 5)]
        public int Quantity { get; set; }
    }
}
