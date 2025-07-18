using System.Threading.Tasks;
using TaskQueue.DAL.Context;
using TaskQueue.DAL.Interfaces;
using TaskQueue.DAL.Repositories;

namespace TaskQueue.DAL.UnitsOfWork
{
    public class UnitOfWork : TaskQueue.DAL.Interfaces.IUnitOfWork
    {
        private readonly TaskQueueContext _context;
        public ITaskRepository Tasks { get; }
        public ITaskLogRepository TaskLogs { get; }
        public INotificationRepository Notifications { get; }

        public UnitOfWork(TaskQueueContext context)
        {
            _context = context;
            Tasks = new TaskQueue.DAL.Repositories.TaskRepository(_context);
            TaskLogs = new TaskQueue.DAL.Repositories.TaskLogRepository(_context);
            Notifications = new TaskQueue.DAL.Repositories.NotificationRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
