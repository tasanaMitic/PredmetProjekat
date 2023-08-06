using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class ProductDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Size { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Sex { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Season { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Guid CategoryId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Guid BrandId { get; set; }
    }
}
