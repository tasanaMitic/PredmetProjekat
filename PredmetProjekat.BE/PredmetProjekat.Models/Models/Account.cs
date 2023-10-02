using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredmetProjekat.Models.Models
{
    public class Account : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        public Account? Manager { get; set; }
        public List<Account> Manages { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

    }
}
