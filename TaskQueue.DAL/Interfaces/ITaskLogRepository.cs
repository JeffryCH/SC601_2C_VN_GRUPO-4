using TaskQueue.ML.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskQueue.DAL.Interfaces
{
    public interface ITaskLogRepository
    {
        System.Threading.Tasks.Task<IEnumerable<TaskLog>> GetAllAsync();
        System.Threading.Tasks.Task<TaskLog?> GetByIdAsync(long id);
        System.Threading.Tasks.Task AddAsync(TaskLog entity);
        System.Threading.Tasks.Task Update(TaskLog entity);
        System.Threading.Tasks.Task Delete(TaskLog entity);
    }
}
