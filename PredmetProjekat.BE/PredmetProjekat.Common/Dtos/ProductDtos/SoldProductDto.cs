using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class SoldProductDto
    {
        public Guid SoldProductId { get; set; }
        public StockedProductDto? Product { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required, Range(1, 5, ErrorMessage = "Value for quantity must be between {1} and {2}.")]
        public int Quantity { get; set; }
    }
}
