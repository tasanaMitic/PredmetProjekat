using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class PriceDto
    {
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Value for quantity must be between {1} and {2}.")]
        public decimal Value { get; set; }
    }
}
