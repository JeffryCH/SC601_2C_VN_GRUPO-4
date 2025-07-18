using System;

namespace TaskQueue.ML.Entities
{
    public partial class TaskLog
    {
        public long Id { get; set; }
        public int TaskId { get; set; }
        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset? FinishedOn { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public virtual Task? Task { get; set; }
    }
}
