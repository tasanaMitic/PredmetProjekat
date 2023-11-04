namespace PredmetProjekat.Common.Dtos.UserDtos
{
    public class EmployeeDto : UserDto
    {
        public Guid ManagerId { get; set; }
        public UserDto Manager { get; set; }
        public IEnumerable<UserDto> Manages { get; set; }
    }
}
