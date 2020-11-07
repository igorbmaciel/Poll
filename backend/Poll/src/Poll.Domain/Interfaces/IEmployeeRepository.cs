using Poll.Domain.Entities;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee employee);
    }
}
