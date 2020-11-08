using Dapper;
using Poll.Domain.Interfaces.ReadRepository;
using Poll.Domain.Queries.Response;
using Poll.Infra.Base;
using Poll.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poll.Infra.Repositories.Read
{
    public class TaskReadRepository : DapperRepositoryBase<PollContext>, ITaskReadRepository
    {
        public TaskReadRepository(
            IDapperProvider dapperProvider)
            : base(dapperProvider)
        {
        }

        public async Task<List<GetTasksResponse>> GetTasksVotes()
        {
            var sqlQuery = @"select v.taskId, t.name as TaskName, e.name as EmployeeName, v.""Date""
                            from public.""Vote"" v
                            join public.""Tasks"" t on t.""TasksId"" = v.TaskId
                            join public.""Employee"" e on e.""EmployeeId"" = v.EmployeeId";

            return (await Connection.QueryAsync<GetTasksResponse>(sqlQuery)).ToList();
        }

        public async Task<List<GetVotesResponse>> GetVotes()
        {
            var sqlQuery = @"select t.name as TaskName, count(*) as QuantityVotes
                            from public.""Vote"" v
                            join public.""Tasks"" t on t.""TasksId"" = v.TaskId
                            group by v.taskId, t.name
                            order by QuantityVotes desc";

            return (await Connection.QueryAsync<GetVotesResponse>(sqlQuery)).ToList();
        }
    }
}
