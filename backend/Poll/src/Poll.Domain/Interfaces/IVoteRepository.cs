using Poll.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces
{
    public interface IVoteRepository
    {
        Task AddVote(Vote vote);
        Task<bool> ValidateVoteByEmployeeId(Guid employeeId);
    }
}
