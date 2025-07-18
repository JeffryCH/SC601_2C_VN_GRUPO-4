using TaskQueue.ML.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskQueue.DAL.Interfaces
{
    public interface INotificationRepository
    {
        System.Threading.Tasks.Task<IEnumerable<Notification>> GetAllAsync();
        System.Threading.Tasks.Task<Notification?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Notification entity);
        System.Threading.Tasks.Task Update(Notification entity);
        System.Threading.Tasks.Task Delete(Notification entity);
    }
}
