using Poll.Domain.Entities;
using System.Threading.Tasks;

namespace Poll.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(Tasks tasks);
    }
}
