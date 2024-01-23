using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class ProductAttribute
    {
        [Key]
        public Guid AttributeId { get; set; }
        [Required]
        public string AttributeName { get; set; }
        //[Required]
        //public string AttributeValue { get; set; }
    }
}
