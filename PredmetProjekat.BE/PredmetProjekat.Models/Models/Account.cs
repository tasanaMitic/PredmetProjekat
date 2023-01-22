using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Account
    {
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
    }
}
