using System;
using System.Collections.Generic;

namespace TaskQueue.ML.Entities
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public byte TaskTypeId { get; set; }
        public byte PriorityId { get; set; }
        public byte StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset ScheduledOn { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset? CompletedOn { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual Priority? Priority { get; set; }
        public virtual TaskStatus? Status { get; set; }
        public virtual TaskType? TaskType { get; set; }
        public virtual ICollection<TaskParameter> Parameters { get; set; } = new List<TaskParameter>();
        public virtual ICollection<TaskLog> Logs { get; set; } = new List<TaskLog>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
