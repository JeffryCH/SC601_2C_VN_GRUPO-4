using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.DAL.Context;
using TaskQueue.DAL.Interfaces;
using TaskQueue.ML.Entities;

namespace TaskQueue.DAL.Repositories
{
    public class TaskLogRepository : ITaskLogRepository
    {
        private readonly TaskQueueContext _context;
        public TaskLogRepository(TaskQueueContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task<IEnumerable<TaskLog>> GetAllAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(_context.TaskLogs);
        }
        public async System.Threading.Tasks.Task<TaskLog?> GetByIdAsync(long id)
        {
            return await _context.TaskLogs.FindAsync(id);
        }
        public async System.Threading.Tasks.Task AddAsync(TaskLog entity)
        {
            await _context.TaskLogs.AddAsync(entity);
        }
        public async System.Threading.Tasks.Task Update(TaskLog entity)
        {
            _context.TaskLogs.Update(entity);
            await System.Threading.Tasks.Task.CompletedTask;
        }
        public async System.Threading.Tasks.Task Delete(TaskLog entity)
        {
            _context.TaskLogs.Remove(entity);
            await System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
