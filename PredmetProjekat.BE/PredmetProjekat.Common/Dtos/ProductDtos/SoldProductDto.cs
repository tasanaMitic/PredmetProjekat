using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class SoldProductDto
    {
        public string ProductId { get; set; }
        [Range(1, 5, ErrorMessage = "Value for quantity must be between {1} and {2}.")]
        public int Quantity { get; set; }
    }
}
