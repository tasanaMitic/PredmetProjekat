using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.IdentityDtos
{
    public class LoginDto
    {
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Your email address is not valid")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Your password is limited to {2} to {1} characters", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
