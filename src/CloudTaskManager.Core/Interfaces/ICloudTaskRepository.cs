using CloudTaskManager.Core.Entities;
using CloudTaskManager.Core.Enums;
namespace CloudTaskManager.Core.Interfaces;

public interface ICloudTaskRepository : IRepository<CloudTask>
{
    Task<IEnumerable<CloudTask>> GetUserTasksAsync(Guid userId);
    Task<IEnumerable<CloudTask>> GetTasksByStatusAsync(CloudTaskManager.Core.Enums.TaskStatus status);
} 