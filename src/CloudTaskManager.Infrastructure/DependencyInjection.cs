using CloudTaskManager.Core.Interfaces;
using CloudTaskManager.Core.Interfaces.Services;
using CloudTaskManager.Infrastructure.Data;
using CloudTaskManager.Infrastructure.Repositories;
using CloudTaskManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudTaskManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Регистрация DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

        // Регистрация репозиториев
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICloudTaskRepository, CloudTaskRepository>();

        // Регистрация AWS сервисов
        services.AddScoped<IAwsService, AwsService>();
        services.AddScoped<IEksService, EksService>();
        services.AddScoped<IS3Service, S3Service>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        await DbInitializer.InitializeAsync(serviceProvider);
    }
} 