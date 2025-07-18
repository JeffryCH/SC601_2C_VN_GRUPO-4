using System;
using System.Threading.Tasks;
using TaskQueue.DAL.Interfaces;

namespace TaskQueue.DAL.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        ITaskLogRepository TaskLogs { get; }
        INotificationRepository Notifications { get; }
        Task<int> CompleteAsync();
    }
}
