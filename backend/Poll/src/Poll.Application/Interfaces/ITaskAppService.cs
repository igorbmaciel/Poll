using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poll.Application.Interfaces
{
    public interface ITaskAppService
    {
        Task<AddTaskResponse> AddTask(AddTaskCommand command);
        Task<List<GetAllTasksResponse>> GetAllTasks();
    }
}
