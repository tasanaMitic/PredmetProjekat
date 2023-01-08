using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Brand
    {
        [Key]
        public Guid BrandId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
