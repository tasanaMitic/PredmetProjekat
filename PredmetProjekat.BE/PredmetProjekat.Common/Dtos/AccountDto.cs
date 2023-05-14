using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos
{
    public class AccountDto : LoginDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

    }
}
