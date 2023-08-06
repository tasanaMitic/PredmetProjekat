using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos
{
    public class RegisterDto
    {
        [Required(AllowEmptyStrings = false)]
        public string RegisterCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Location { get; set; }
    }
}
