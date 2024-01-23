using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class ProductTypeDtoId
    {
        [Required]
        public Guid ProductTypeId { get; set; }
        public string Name { get; set; }
        public IEnumerable<AttributeDto> Attributes { get; set; }
    }
}
