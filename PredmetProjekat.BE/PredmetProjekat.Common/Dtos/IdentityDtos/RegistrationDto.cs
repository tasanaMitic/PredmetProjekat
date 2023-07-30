using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.IdentityDtos
{
    public class RegistrationDto : LoginDto
    {
        [Required]
        [StringLength(12, ErrorMessage = "Your username is limited to {2} to {1} characters.", MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$", ErrorMessage = "Characters are not allowed in first name.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$", ErrorMessage = "Characters are not allowed in last name.")]
        public string LastName { get; set; }
    }
}
