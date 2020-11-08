using Microsoft.EntityFrameworkCore;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Infra.Context;
using System;
using System.Collections.Generic;
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

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await Context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(Guid id)
        {
            return await Context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
