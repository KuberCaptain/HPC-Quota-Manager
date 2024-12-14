using CloudTaskManager.Core.Entities;
using CloudTaskManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudTaskManager.Infrastructure.Repositories;

public class CloudTaskRepository : Repository<CloudTask>, ICloudTaskRepository
{
    private readonly ApplicationDbContext _context;

    public CloudTaskRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CloudTask>> GetUserTasksAsync(Guid userId)
    {
        return await _context.CloudTasks
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CloudTask>> GetTasksByStatusAsync(TaskStatus status)
    {
        return await _context.CloudTasks
            .Where(t => t.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<CloudTask>> GetTasksByPriorityAsync(TaskPriority priority)
    {
        return await _context.CloudTasks
            .Where(t => t.Priority == priority)
            .ToListAsync();
    }

    public async Task<IEnumerable<CloudTask>> GetOverdueTasks()
    {
        var currentDate = DateTime.UtcNow;
        return await _context.CloudTasks
            .Where(t => t.DueDate < currentDate && t.Status != TaskStatus.Completed)
            .ToListAsync();
    }

    public async Task<IEnumerable<CloudTask>> GetTasksDueInNextDaysAsync(int days)
    {
        var currentDate = DateTime.UtcNow;
        var futureDate = currentDate.AddDays(days);
        
        return await _context.CloudTasks
            .Where(t => t.DueDate >= currentDate && 
                       t.DueDate <= futureDate && 
                       t.Status != TaskStatus.Completed)
            .ToListAsync();
    }

    public async Task<int> GetUserTasksCountAsync(Guid userId)
    {
        return await _context.CloudTasks
            .CountAsync(t => t.UserId == userId);
    }

    public async Task<IEnumerable<CloudTask>> GetTasksByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.CloudTasks
            .Where(t => t.DueDate >= startDate && t.DueDate <= endDate)
            .ToListAsync();
    }
} 