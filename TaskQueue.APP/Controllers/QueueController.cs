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
           
            return View(tasks);
        }

        public async Task<IActionResult> History()
        {
      
            var tasks = await _taskService.GetAllAsync();
            return View(tasks);
        }
    }
}
