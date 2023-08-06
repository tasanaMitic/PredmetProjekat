using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos
{
    public  class BrandDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
