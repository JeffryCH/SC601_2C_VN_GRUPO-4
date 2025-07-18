using System.Collections.Generic;
using System.Threading.Tasks;
using MLTask = TaskQueue.ML.Entities.Task;

namespace TaskQueue.BLL.Interfaces
{
    using MLTask = TaskQueue.ML.Entities.Task;
    public interface ITaskService
    {
        System.Threading.Tasks.Task<IEnumerable<MLTask>> GetAllAsync();
        System.Threading.Tasks.Task<MLTask?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(MLTask entity);
        System.Threading.Tasks.Task Update(MLTask entity);
        System.Threading.Tasks.Task Delete(MLTask entity);
        System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.Priority>> GetPrioritiesAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.TaskType>> GetTaskTypesAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.TaskStatus>> GetStatusesAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.User>> GetUsersAsync();
        System.Threading.Tasks.Task AddUserAsync(TaskQueue.ML.Entities.User user);
    }
}
