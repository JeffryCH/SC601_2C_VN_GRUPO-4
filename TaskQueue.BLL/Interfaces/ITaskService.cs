    using System.Collections.Generic;
using System.Threading.Tasks;
using MLTask = TaskQueue.ML.Entities.Task;

namespace TaskQueue.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<MLTask>> GetAllAsync();
        Task<MLTask?> GetByIdAsync(int id);
        Task AddAsync(MLTask entity);
        Task Update(MLTask entity);
        Task Delete(MLTask entity);
        
        Task<IEnumerable<TaskQueue.ML.Entities.Priority>> GetPrioritiesAsync();
        Task<IEnumerable<TaskQueue.ML.Entities.User>> GetUsersAsync();
        Task<IEnumerable<TaskQueue.ML.Entities.TaskType>> GetTaskTypesAsync();
        Task<IEnumerable<TaskQueue.ML.Entities.TaskStatus>> GetStatusesAsync();
        Task AddUserAsync(TaskQueue.ML.Entities.User user);
        
        // Métodos para cambio automático de estados
        Task UpdateTaskStatusesAutomaticallyAsync();
        Task<bool> ShouldStartTaskAsync(MLTask task);
        Task<bool> ShouldFailTaskAsync(MLTask task);
        Task StartTaskAsync(int taskId);
        Task CompleteTaskAsync(int taskId);
        Task FailTaskAsync(int taskId);
    }
}
