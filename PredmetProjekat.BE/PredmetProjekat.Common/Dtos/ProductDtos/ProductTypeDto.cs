using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class ProductTypeDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required, MinLength(3)]
        public IEnumerable<string> Attributes { get; set; }
    }
}
