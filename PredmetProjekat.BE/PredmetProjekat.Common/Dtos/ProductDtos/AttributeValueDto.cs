using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class AttributeValueDto
    {
        [Required(AllowEmptyStrings = false)]
        public Guid AttributeId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string AttributeValue { get; set; }
        public string ?AttributeName { get; set; }
    }
}
