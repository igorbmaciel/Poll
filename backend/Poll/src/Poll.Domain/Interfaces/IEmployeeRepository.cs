using Poll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(Guid id);
    }
}
