using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Account
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
