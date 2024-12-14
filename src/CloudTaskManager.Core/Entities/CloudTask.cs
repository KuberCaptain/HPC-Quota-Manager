using CloudTaskManager.Core.Enums;

namespace CloudTaskManager.Core.Entities;

public class CloudTask
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
} 