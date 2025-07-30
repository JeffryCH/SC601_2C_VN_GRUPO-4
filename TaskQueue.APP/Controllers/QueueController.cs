using Microsoft.AspNetCore.Mvc;
using TaskQueue.BLL.Interfaces;
using System.Threading.Tasks;

namespace TaskQueue.APP.Controllers
{
    public class QueueController : Controller
    {
        private readonly ITaskService _taskService;
        public QueueController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllAsync();
            // Aquí se pueden agrupar por prioridad y estado si lo requiere la vista
            return View(tasks);
        }

        public async Task<IActionResult> History()
        {
            // Aquí se mostraría el historial de ejecución de tareas
            var tasks = await _taskService.GetAllAsync();
            return View(tasks);
        }
    }
}
