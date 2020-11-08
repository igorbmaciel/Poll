using Poll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(Tasks tasks);
        Task<List<Tasks>> GetAllTasks();
        Task<Tasks> GetTasksById(Guid id);
    }
}
