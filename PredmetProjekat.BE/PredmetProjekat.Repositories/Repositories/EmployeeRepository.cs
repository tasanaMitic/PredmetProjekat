using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(StoreContext context) : base(context)
        {
        }

        public bool AssignManager(string managerUsername, string employeeUsername)
        {
            try
            {
                var employee = _context.Employees.Find(employeeUsername);
                var manager = _context.Employees.Find(managerUsername);

                if (employee == null || manager == null || manager.Equals(employee))
                {
                    return false;
                }

                employee.Manager = manager;
                _context.Employees.Update(employee);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveManager(string employeeUsername)  //TODO?
        {
            try
            {
                var employee = _context.Employees.Find(employeeUsername);

                if (employee == null)
                {
                    return false;
                }

                employee.ManagerUsername = null;
                _context.Employees.Update(employee);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
