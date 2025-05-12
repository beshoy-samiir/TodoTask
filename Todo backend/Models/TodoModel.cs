using System.ComponentModel.DataAnnotations;

namespace Todo_backend.Models
{
    public enum Status { Pending, InProgress, Completed }
    public enum Priority { Low, Medium, High }
    public class TodoModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public Priority Priority { get; set; } = Priority.Medium;
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}


