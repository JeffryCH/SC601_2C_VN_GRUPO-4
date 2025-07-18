using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.ML.Entities;

namespace TaskQueue.BLL.Interfaces
{
    public interface ITaskLogService
    {
        System.Threading.Tasks.Task<IEnumerable<TaskLog>> GetAllAsync();
        System.Threading.Tasks.Task<TaskLog?> GetByIdAsync(long id);
        System.Threading.Tasks.Task AddAsync(TaskLog entity);
        System.Threading.Tasks.Task Update(TaskLog entity);
        System.Threading.Tasks.Task Delete(TaskLog entity);
    }
}
