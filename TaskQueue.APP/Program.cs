

using Microsoft.EntityFrameworkCore;
using TaskQueue.DAL.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TaskQueue.DAL.Context.TaskQueueContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

// Registro de dependencias
builder.Services.AddScoped<TaskQueue.DAL.Interfaces.IUnitOfWork, TaskQueue.DAL.UnitsOfWork.UnitOfWork>();
builder.Services.AddScoped<TaskQueue.BLL.Interfaces.ITaskService, TaskQueue.BLL.Servicios.TaskService>();
builder.Services.AddScoped<TaskQueue.BLL.Interfaces.ITaskLogService, TaskQueue.BLL.Servicios.TaskLogService>();
builder.Services.AddScoped<TaskQueue.BLL.Interfaces.INotificationService, TaskQueue.BLL.Servicios.NotificationService>();

var app = builder.Build();

// Inicialización automática de datos críticos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskQueue.DAL.Context.TaskQueueContext>();
    // Prioridades
    if (!context.Priorities.Any())
    {
        context.Priorities.AddRange(new[] {
            new TaskQueue.ML.Entities.Priority { Id = 1, Name = "Baja" },
            new TaskQueue.ML.Entities.Priority { Id = 2, Name = "Media" },
            new TaskQueue.ML.Entities.Priority { Id = 3, Name = "Alta" }
        });
    }
    // Estados
    if (!context.TaskStatuses.Any())
    {
        context.TaskStatuses.AddRange(new[] {
            new TaskQueue.ML.Entities.TaskStatus { Id = 1, Name = "Pendiente" },
            new TaskQueue.ML.Entities.TaskStatus { Id = 2, Name = "En Proceso" },
            new TaskQueue.ML.Entities.TaskStatus { Id = 3, Name = "Finalizada" },
            new TaskQueue.ML.Entities.TaskStatus { Id = 4, Name = "Fallida" }
        });
    }
    // Tipos de tarea
    if (!context.TaskTypes.Any())
    {
        context.TaskTypes.AddRange(new[] {
            new TaskQueue.ML.Entities.TaskType { Id = 1, Name = "Procesar Datos" },
            new TaskQueue.ML.Entities.TaskType { Id = 2, Name = "Enviar Correo" },
            new TaskQueue.ML.Entities.TaskType { Id = 3, Name = "Generar Reporte" },
            new TaskQueue.ML.Entities.TaskType { Id = 4, Name = "Ejecutar Script" }
        });
    }
    // Usuario
    if (!context.Users.Any())
    {
        context.Users.Add(new TaskQueue.ML.Entities.User { FullName = "Admin", Email = "admin@demo.com", CreatedAt = DateTimeOffset.Now });
    }
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
