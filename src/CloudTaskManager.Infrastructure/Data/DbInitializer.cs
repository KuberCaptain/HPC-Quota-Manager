using CloudTaskManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CloudTaskManager.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

            // Проверяем, есть ли данные
            if (!await context.Users.AnyAsync())
            {
                // Добавляем тестового пользователя
                await context.Users.AddAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbHJwQUZr2qNKpABZji1LJgZxeVQQBqVMI2YZZQYHUZGUaHSJZ9gXZG6PNh0TLyNQ==", // пароль: Admin123!
                    FirstName = "Admin",
                    LastName = "User",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                });

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Произошла ошибка при инициализации базы данных.");
            throw;
        }
    }
} 