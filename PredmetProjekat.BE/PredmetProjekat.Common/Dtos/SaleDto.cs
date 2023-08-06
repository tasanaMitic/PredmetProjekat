using PredmetProjekat.Common.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetProjekat.Common.Dtos
{
    public class SaleDto
    {
        public Guid RegisterId { get; set; }
        public IEnumerable<SoldProductDto> SoldProducts { get; set; }
    }
}
