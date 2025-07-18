using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.ML.Entities;

namespace TaskQueue.BLL.Interfaces
{
    public interface INotificationService
    {
        System.Threading.Tasks.Task<IEnumerable<Notification>> GetAllAsync();
        System.Threading.Tasks.Task<Notification?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Notification entity);
        System.Threading.Tasks.Task Update(Notification entity);
        System.Threading.Tasks.Task Delete(Notification entity);
    }
}
