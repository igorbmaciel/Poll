using Poll.Domain.Entities;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces
{
    public interface IVoteRepository
    {
        Task AddVote(Vote vote);
    }
}
