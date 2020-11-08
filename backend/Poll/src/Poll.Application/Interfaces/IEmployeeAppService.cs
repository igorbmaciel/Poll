using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poll.Application.Interfaces
{
    public interface IEmployeeAppService
    {
        Task<AddEmployeeResponse> AddEmployee(AddEmployeeCommand command);
        Task<List<GetAllEmployeeResponse>> GetAllEmployees();
    }
}
