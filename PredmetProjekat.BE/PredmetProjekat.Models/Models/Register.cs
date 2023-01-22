using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Register
    {
        [Key]
        public Guid RegisterId { get; set; }
        [Required]
        public string RegisterCode { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
