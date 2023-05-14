using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Models.Models
{
    public class Account : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
