using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Sex { get; set; }
        public string Season { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}
