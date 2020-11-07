using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Infra.Context;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Poll.Infra.Repositories
{
    public class EmployeeRepository : EfCoreRepositoryBase<PollContext, Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbContextProvider<PollContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task AddEmployee(Employee employee)
        {
            Context.Add(employee);
            await Context.SaveChangesAsync();
        }
    }
}
