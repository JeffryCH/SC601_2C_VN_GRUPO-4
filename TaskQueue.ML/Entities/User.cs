using System;

namespace TaskQueue.ML.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
