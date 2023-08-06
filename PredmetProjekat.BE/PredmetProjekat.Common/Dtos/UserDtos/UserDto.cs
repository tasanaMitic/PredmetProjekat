using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.UserDtos
{
    public class UserDto
    {
        [Required]
        [StringLength(12, ErrorMessage = "Your username is limited to {2} to {1} characters.", MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$", ErrorMessage = "Characters are not allowed in first name.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$", ErrorMessage = "Characters are not allowed inlast name.")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Your email address is not valid.")]
        public string Email { get; set; }
    }
}
