using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.BLL.Interfaces;
using TaskQueue.DAL.UnitsOfWork;
using TaskQueue.ML.Entities;

namespace TaskQueue.BLL.Servicios
{
    public class TaskLogService : ITaskLogService
    {
        private readonly TaskQueue.DAL.Interfaces.IUnitOfWork _unitOfWork;
        public TaskLogService(TaskQueue.DAL.Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public System.Threading.Tasks.Task<IEnumerable<TaskLog>> GetAllAsync()
        {
            return _unitOfWork.TaskLogs.GetAllAsync();
        }

        public System.Threading.Tasks.Task<TaskLog?> GetByIdAsync(long id)
        {
            return _unitOfWork.TaskLogs.GetByIdAsync(id);
        }

        public System.Threading.Tasks.Task AddAsync(TaskLog entity)
        {
            return AddAsyncInternal(entity);
        }

        private async System.Threading.Tasks.Task AddAsyncInternal(TaskLog entity)
        {
            await _unitOfWork.TaskLogs.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public System.Threading.Tasks.Task Update(TaskLog entity)
        {
            return UpdateInternal(entity);
        }

        private async System.Threading.Tasks.Task UpdateInternal(TaskLog entity)
        {
            await System.Threading.Tasks.Task.Run(() => _unitOfWork.TaskLogs.Update(entity));
            await _unitOfWork.CompleteAsync();
        }

        public System.Threading.Tasks.Task Delete(TaskLog entity)
        {
            return DeleteInternal(entity);
        }

        private async System.Threading.Tasks.Task DeleteInternal(TaskLog entity)
        {
            await System.Threading.Tasks.Task.Run(() => _unitOfWork.TaskLogs.Delete(entity));
            await _unitOfWork.CompleteAsync();
        }
    }
}
