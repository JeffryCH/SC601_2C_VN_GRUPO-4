using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.DAL.Context;
using TaskQueue.DAL.Interfaces;
using TaskQueue.ML.Entities;

namespace TaskQueue.DAL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskQueueContext _context;
        public NotificationRepository(TaskQueueContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(_context.Notifications);
        }
        public async System.Threading.Tasks.Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }
        public async System.Threading.Tasks.Task AddAsync(Notification entity)
        {
            await _context.Notifications.AddAsync(entity);
        }
        public async System.Threading.Tasks.Task Update(Notification entity)
        {
            _context.Notifications.Update(entity);
            await System.Threading.Tasks.Task.CompletedTask;
        }
        public async System.Threading.Tasks.Task Delete(Notification entity)
        {
            _context.Notifications.Remove(entity);
            await System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
