using System.Collections.Generic;
using System.Linq;
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
            // Actualizar estados antes de obtener las tareas
            await UpdateTaskStatusesAutomaticallyAsync();
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

        // NUEVOS MÉTODOS PARA CAMBIO AUTOMÁTICO DE ESTADOS

        public async Task UpdateTaskStatusesAutomaticallyAsync()
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            var statuses = await GetStatusesAsync();
            
            var pendingStatus = statuses.FirstOrDefault(s => s.Name == "Pendiente");
            var inProgressStatus = statuses.FirstOrDefault(s => s.Name == "En Proceso");
            var failedStatus = statuses.FirstOrDefault(s => s.Name == "Fallida");

            if (pendingStatus == null || inProgressStatus == null || failedStatus == null)
                return;

            bool hasChanges = false;

            foreach (var task in tasks)
            {
                // Cambiar de Pendiente a en proce oceso si llegó la fecha programada
                if (task.StatusId == pendingStatus.Id && await ShouldStartTaskAsync(task))
                {
                    task.StatusId = inProgressStatus.Id;
                    task.StartedOn = DateTimeOffset.Now;
                    task.UpdatedAt = DateTimeOffset.Now;
                    _unitOfWork.Tasks.Update(task);
                    hasChanges = true;
                }
                // Cambiar a Fallida si lleva mucho tiempo sin completarse
                else if ((task.StatusId == pendingStatus.Id || task.StatusId == inProgressStatus.Id) && 
                         await ShouldFailTaskAsync(task))
                {
                    task.StatusId = failedStatus.Id;
                    task.UpdatedAt = DateTimeOffset.Now;
                    _unitOfWork.Tasks.Update(task);
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> ShouldStartTaskAsync(MLTask task)
        {
            // La tarea debe comenzar si:
            // 1. Está en estado Pendiente
            // 2. Ya llegó o pasó la fecha/hora programada
            return task.ScheduledOn <= DateTimeOffset.Now;
        }

        public async Task<bool> ShouldFailTaskAsync(MLTask task)
        {
            var priorities = await GetPrioritiesAsync();
            var taskPriority = priorities.FirstOrDefault(p => p.Id == task.PriorityId);
            
            // Definir límites de tiempo según prioridad
            var hoursLimit = taskPriority?.Name switch
            {
                "Alta" => 4,    // 4 horas para alta prioridad
                "Media" => 24,  // 24 horas para media prioridad
                "Baja" => 72,   // 72 horas para baja prioridad
                _ => 24
            };

            var timeLimit = task.ScheduledOn.AddHours(hoursLimit);
            
            // La tarea falla si:
            // 1. Ha pasado el tiempo límite desde la fecha programada
            // 2. No está completada ni fallida ya
            return DateTimeOffset.Now > timeLimit;
        }

        public async Task StartTaskAsync(int taskId)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null) return;

            var statuses = await GetStatusesAsync();
            var inProgressStatus = statuses.FirstOrDefault(s => s.Name == "En Proceso");
            
            if (inProgressStatus != null)
            {
                task.StatusId = inProgressStatus.Id;
                task.StartedOn = DateTimeOffset.Now;
                task.UpdatedAt = DateTimeOffset.Now;
                await Update(task);
            }
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null) return;

            var statuses = await GetStatusesAsync();
            var completedStatus = statuses.FirstOrDefault(s => s.Name == "Finalizada");
            
            if (completedStatus != null)
            {
                task.StatusId = completedStatus.Id;
                task.CompletedOn = DateTimeOffset.Now;
                task.UpdatedAt = DateTimeOffset.Now;
                await Update(task);
            }
        }

        public async Task FailTaskAsync(int taskId)
        {
            var task = await GetByIdAsync(taskId);
            if (task == null) return;

            var statuses = await GetStatusesAsync();
            var failedStatus = statuses.FirstOrDefault(s => s.Name == "Fallida");
            
            if (failedStatus != null)
            {
                task.StatusId = failedStatus.Id;
                task.UpdatedAt = DateTimeOffset.Now;
                await Update(task);
            }
        }
    }
}
