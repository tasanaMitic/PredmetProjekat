using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class ProductType
    {
        [Key]
        public Guid ProductTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IEnumerable<ProductAttribute> Attributes { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
