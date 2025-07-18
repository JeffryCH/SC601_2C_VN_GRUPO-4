using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskQueue.DAL.Context;
using TaskQueue.DAL.Interfaces;
using TaskQueue.ML.Entities;

namespace TaskQueue.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskQueueContext _context;

        public TaskRepository(TaskQueueContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskQueue.ML.Entities.TaskStatus>> GetStatusesAsync()
        {
            return await _context.TaskStatuses.ToListAsync();
        }

        public async System.Threading.Tasks.Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskQueue.ML.Entities.Task>> GetAllAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(
                _context.Tasks
                    .Include(t => t.Priority)
                    .Include(t => t.Status)
                    .ToList()
            );
        }

        public async System.Threading.Tasks.Task<TaskQueue.ML.Entities.Task?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async System.Threading.Tasks.Task AddAsync(TaskQueue.ML.Entities.Task entity)
        {
            await _context.Tasks.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task Update(TaskQueue.ML.Entities.Task entity)
        {
            _context.Tasks.Update(entity);
            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task Delete(TaskQueue.ML.Entities.Task entity)
        {
            _context.Tasks.Remove(entity);
            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Priority>> GetPrioritiesAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(_context.Priorities.ToList());
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetUsersAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(_context.Users.ToList());
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskType>> GetTaskTypesAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(_context.TaskTypes.ToList());
        }
    }
}
