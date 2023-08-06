using PredmetProjekat.Common.Dtos.ProductDtos;
using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos
{
    public class SaleDto
    {

        [Required(AllowEmptyStrings = false)]
        public Guid RegisterId { get; set; }
        public IEnumerable<SoldProductDto> SoldProducts { get; set; }
    }
}
