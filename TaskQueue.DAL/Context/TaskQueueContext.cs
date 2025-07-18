using Microsoft.EntityFrameworkCore;
using TaskQueue.ML.Entities;
using MLTask = TaskQueue.ML.Entities.Task;
using MLTaskStatus = TaskQueue.ML.Entities.TaskStatus;

namespace TaskQueue.DAL.Context
{
    public partial class TaskQueueContext : DbContext
    {
        public TaskQueueContext()
        {
        }

        public TaskQueueContext(DbContextOptions<TaskQueueContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Priority> Priorities { get; set; } = null!;
        public virtual DbSet<MLTaskStatus> TaskStatuses { get; set; } = null!;
        public virtual DbSet<TaskType> TaskTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<MLTask> Tasks { get; set; } = null!;
        public virtual DbSet<TaskParameter> TaskParameters { get; set; } = null!;
        public virtual DbSet<TaskLog> TaskLogs { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes agregar configuraciones adicionales si lo requiere el modelo
            modelBuilder.Entity<MLTask>().ToTable("Task");
            modelBuilder.Entity<Priority>().ToTable("Priority");
            modelBuilder.Entity<MLTaskStatus>().ToTable("TaskStatus");
            modelBuilder.Entity<TaskType>().ToTable("TaskType");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<TaskParameter>().ToTable("TaskParameter");
            modelBuilder.Entity<TaskLog>().ToTable("TaskLog");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<MLTask>()
                .HasOne(t => t.CreatedByNavigation)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            // ...existing code...
        }
    }
}
