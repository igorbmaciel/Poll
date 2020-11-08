using Microsoft.EntityFrameworkCore;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Infra.Context;
using System;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Poll.Infra.Repositories
{
    public class VoteRepository : EfCoreRepositoryBase<PollContext, Vote>, IVoteRepository
    {
        public VoteRepository(IDbContextProvider<PollContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task AddVote(Vote vote)
        {
            Context.Add(vote);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> ValidateVoteByEmployeeId(Guid employeeId)
        {
            return await Context.Votes.AnyAsync(x => x.EmployeeId == employeeId);
        }
    }
}
