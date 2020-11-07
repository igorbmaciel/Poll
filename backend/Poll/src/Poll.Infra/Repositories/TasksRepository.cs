using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Infra.Context;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Poll.Infra.Repositories
{
    public class TasksRepository : EfCoreRepositoryBase<PollContext, Tasks>, ITaskRepository
    {
        public TasksRepository(IDbContextProvider<PollContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task AddTask(Tasks tasks)
        {
            Context.Add(tasks);
            await Context.SaveChangesAsync();
        }
    }
}
