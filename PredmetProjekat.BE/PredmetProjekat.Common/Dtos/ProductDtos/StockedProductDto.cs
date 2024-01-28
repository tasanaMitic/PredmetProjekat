using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class StockedProductDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public CategoryDtoId Category { get; set; }
        public BrandDtoId Brand { get; set; }
        public bool IsInStock { get; set; }
        public decimal Price { get; set; }
        public ProductTypeDtoId ProductType { get; set; }
        public IEnumerable<AttributeValueDto> AttributeValues { get; set; }
    }
}
