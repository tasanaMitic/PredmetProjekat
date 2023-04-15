using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        bool AssignManager(string managerUsername,string employeeUsername);
        bool RemoveManager(string employeeUsername);
    }
}
