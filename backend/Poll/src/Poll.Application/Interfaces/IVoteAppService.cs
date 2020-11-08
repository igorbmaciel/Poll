using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Threading.Tasks;

namespace Poll.Application.Interfaces
{
    public interface IVoteAppService
    {
        Task<AddVoteResponse> AddVote(AddVoteCommand command);
    }
}
