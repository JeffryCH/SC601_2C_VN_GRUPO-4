using TaskQueue.ML.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskQueue.DAL.Interfaces
{
    using MLTask = TaskQueue.ML.Entities.Task;
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task<IEnumerable<MLTask>> GetAllAsync();
        System.Threading.Tasks.Task<MLTask?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(MLTask entity);
        System.Threading.Tasks.Task Update(MLTask entity);
        System.Threading.Tasks.Task Delete(MLTask entity);
        System.Threading.Tasks.Task<IEnumerable<Priority>> GetPrioritiesAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskType>> GetTaskTypesAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.TaskStatus>> GetStatusesAsync();
        System.Threading.Tasks.Task<IEnumerable<User>> GetUsersAsync();
        System.Threading.Tasks.Task AddUserAsync(User user);
    }
}
