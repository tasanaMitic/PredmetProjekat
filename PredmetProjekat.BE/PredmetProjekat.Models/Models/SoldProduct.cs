using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredmetProjekat.Models.Models
{
    public class SoldProduct
    {
        [Key]
        public Guid SoldProductId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Quantity { get; set; }
    }
}
