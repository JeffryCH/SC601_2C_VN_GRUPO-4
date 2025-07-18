using System;

namespace TaskQueue.ML.Entities
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public string SentTo { get; set; } = null!;
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public bool IsSuccess { get; set; }
        public virtual Task? Task { get; set; }
    }
}
