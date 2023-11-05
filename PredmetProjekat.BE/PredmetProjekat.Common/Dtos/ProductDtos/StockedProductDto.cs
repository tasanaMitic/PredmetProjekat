using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class StockedProductDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Size { get; set; }
        public string Sex { get; set; }
        public string Season { get; set; }
        public CategoryDtoId Category { get; set; }
        public BrandDtoId Brand { get; set; }
        public bool IsInStock { get; set; }
    }
}
