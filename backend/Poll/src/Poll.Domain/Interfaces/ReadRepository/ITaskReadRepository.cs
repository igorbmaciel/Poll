using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces.ReadRepository
{
    public interface ITaskReadRepository
    {
        Task<List<GetTasksResponse>> GetTasksVotes();
        Task<List<GetVotesResponse>> GetVotes();
    }
}
