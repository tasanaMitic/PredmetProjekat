using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredmetProjekat.Models.Models
{
    public class SoldProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SoldProductId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Quantity { get; set; }
    }
}
