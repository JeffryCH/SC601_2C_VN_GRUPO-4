using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.BLL.Interfaces;
using TaskQueue.DAL.Interfaces;
using Notification = TaskQueue.ML.Entities.Notification;

namespace TaskQueue.BLL.Servicios
{
    public class NotificationService : INotificationService
    {
        private readonly TaskQueue.DAL.Interfaces.IUnitOfWork _unitOfWork;
        public NotificationService(TaskQueue.DAL.Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public System.Threading.Tasks.Task<IEnumerable<Notification>> GetAllAsync()
        {
            return _unitOfWork.Notifications.GetAllAsync();
        }

        public System.Threading.Tasks.Task<Notification?> GetByIdAsync(int id)
        {
            return _unitOfWork.Notifications.GetByIdAsync(id);
        }

        public System.Threading.Tasks.Task AddAsync(Notification entity)
        {
            return AddAsyncInternal(entity);
        }

        private async System.Threading.Tasks.Task AddAsyncInternal(Notification entity)
        {
            await _unitOfWork.Notifications.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public System.Threading.Tasks.Task Update(Notification entity)
        {
            return UpdateInternal(entity);
        }

        private async System.Threading.Tasks.Task UpdateInternal(Notification entity)
        {
            await System.Threading.Tasks.Task.Run(() => _unitOfWork.Notifications.Update(entity));
            await _unitOfWork.CompleteAsync();
        }

        public System.Threading.Tasks.Task Delete(Notification entity)
        {
            return DeleteInternal(entity);
        }

        private async System.Threading.Tasks.Task DeleteInternal(Notification entity)
        {
            await System.Threading.Tasks.Task.Run(() => _unitOfWork.Notifications.Delete(entity));
            await _unitOfWork.CompleteAsync();
        }
    }
}
