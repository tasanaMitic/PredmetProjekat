namespace PredmetProjekat.Common.Dtos
{
    public class EmployeeDto : UserDto
    {
        public Guid ManagerId { get; set; }
        public UserDto Manager { get; set; }
    }
}
