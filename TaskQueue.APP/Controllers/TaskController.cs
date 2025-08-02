using Microsoft.AspNetCore.Mvc;
using TaskQueue.BLL.Interfaces;
using MLTask = TaskQueue.ML.Entities.Task;
using System.Threading.Tasks;

namespace TaskQueue.APP.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllAsync();
            return View(tasks);
        }

        public async Task<IActionResult> Dashboard()
        {
            var tasks = await _taskService.GetAllAsync();
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Priorities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetPrioritiesAsync(), "Id", "Name");
            ViewBag.TaskTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetTaskTypesAsync(), "Id", "Name");
            ViewBag.Statuses = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetStatusesAsync(), "Id", "Name");
            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetUsersAsync(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MLTask entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.StatusId == 0) entity.StatusId = 1; // Estado "Pendiente" por defecto
                if (entity.ScheduledOn == default) entity.ScheduledOn = DateTimeOffset.Now;
                entity.CreatedAt = DateTimeOffset.Now;
                var users = await _taskService.GetUsersAsync();
                if (!users.Any())
                {
                    // Si no hay usuarios, crear uno y obtener su Id
                    var newUser = new TaskQueue.ML.Entities.User { FullName = "Admin", Email = "admin@demo.com", CreatedAt = DateTimeOffset.Now };
                    await _taskService.AddUserAsync(newUser);
                    entity.CreatedBy = newUser.Id;
                }
                else
                {
                    entity.CreatedBy = users.First().Id;
                }
                await _taskService.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Priorities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetPrioritiesAsync(), "Id", "Name", entity.PriorityId);
            ViewBag.TaskTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetTaskTypesAsync(), "Id", "Name", entity.TaskTypeId);
            ViewBag.Statuses = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetStatusesAsync(), "Id", "Name", entity.StatusId);
            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetUsersAsync(), "Id", "FullName", entity.CreatedBy);

            return View(entity);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            ViewBag.Priorities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetPrioritiesAsync(), "Id", "Name", task.PriorityId);
            ViewBag.TaskTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetTaskTypesAsync(), "Id", "Name", task.TaskTypeId);
            ViewBag.Statuses = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetStatusesAsync(), "Id", "Name", task.StatusId);
            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetUsersAsync(), "Id", "FullName", task.CreatedBy);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MLTask entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.ScheduledOn == default) entity.ScheduledOn = DateTimeOffset.Now;
                await _taskService.Update(entity);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Priorities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetPrioritiesAsync(), "Id", "Name", entity.PriorityId);
            ViewBag.TaskTypes = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetTaskTypesAsync(), "Id", "Name", entity.TaskTypeId);
            ViewBag.Statuses = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetStatusesAsync(), "Id", "Name", entity.StatusId);
            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _taskService.GetUsersAsync(), "Id", "FullName", entity.CreatedBy);
            entity.UpdatedAt = DateTimeOffset.Now; 
            return View(entity);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task != null)
            {
                await _taskService.Delete(task);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> StartTask(int id)
        {
            await _taskService.StartTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CompleteTask(int id)
        {
            await _taskService.CompleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> FailTask(int id)
        {
            await _taskService.FailTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }   

        [HttpPost]
        public async Task<IActionResult> UpdateAllTaskStatuses()
        {
            await _taskService.UpdateTaskStatusesAutomaticallyAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
