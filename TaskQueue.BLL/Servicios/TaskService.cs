using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.BLL.Interfaces;
using TaskQueue.DAL.UnitsOfWork;
using MLTask = TaskQueue.ML.Entities.Task;

namespace TaskQueue.BLL.Servicios
{
    public class TaskService : ITaskService
    {
        private readonly TaskQueue.DAL.Interfaces.IUnitOfWork _unitOfWork;

        public TaskService(TaskQueue.DAL.Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.TaskStatus>> GetStatusesAsync()
        {
            return await _unitOfWork.Tasks.GetStatusesAsync();
        }

        public async System.Threading.Tasks.Task AddUserAsync(TaskQueue.ML.Entities.User user)
        {
            await _unitOfWork.Tasks.AddUserAsync(user);
            await _unitOfWork.CompleteAsync();
        }

    public async Task<IEnumerable<MLTask>> GetAllAsync()
    {
        return await _unitOfWork.Tasks.GetAllAsync();
    }

    public async Task<MLTask?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Tasks.GetByIdAsync(id);
    }

    public async Task AddAsync(MLTask entity)
    {
        await _unitOfWork.Tasks.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Update(MLTask entity)
    {
        await System.Threading.Tasks.Task.Run(() => _unitOfWork.Tasks.Update(entity));
        await _unitOfWork.CompleteAsync();
    }

    public async Task Delete(MLTask entity)
    {
        await System.Threading.Tasks.Task.Run(() => _unitOfWork.Tasks.Delete(entity));
        await _unitOfWork.CompleteAsync();
    }
    public async Task<IEnumerable<TaskQueue.ML.Entities.Priority>> GetPrioritiesAsync()
    {
        return await _unitOfWork.Tasks.GetPrioritiesAsync();
    }

    public async Task<IEnumerable<TaskQueue.ML.Entities.User>> GetUsersAsync()
    {
        return await _unitOfWork.Tasks.GetUsersAsync();
    }

    public async Task<IEnumerable<TaskQueue.ML.Entities.TaskType>> GetTaskTypesAsync()
    {
        return await _unitOfWork.Tasks.GetTaskTypesAsync();
    }
}
}
