namespace TaskQueue.ML.Entities
{
    public partial class TaskParameter
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
        public virtual Task? Task { get; set; }
    }
}
