using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos
{
    public class Quantity
    {
        [Range(1, 50, ErrorMessage = "Value for quantity must be between {1} and {2}.")]
        public int Value { get; set; }
    }
}
