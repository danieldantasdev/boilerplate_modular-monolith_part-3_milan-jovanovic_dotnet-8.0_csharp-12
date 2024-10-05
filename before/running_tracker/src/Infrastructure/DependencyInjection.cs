using Application.Abstractions.Caching;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Notifications;
using Domain.Activities;
using Domain.Followers;
using Domain.Users;
using Domain.Workouts;
using Infrastructure.Caching;
using Infrastructure.Database;
using Infrastructure.Messaging;
using Infrastructure.Notifications;
using Infrastructure.Outbox;
using Infrastructure.Repositories;
using Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();

        services.AddTransient<INotificationService, NotificationService>();

        string redisConnectionString = configuration.GetConnectionString("Cache")!;

        services.AddStackExchangeRedisCache(options =>
            options.Configuration = redisConnectionString);

        services.AddSingleton<ICacheService, CacheService>();

        services.AddHealthChecks()
            .AddNpgSql(connectionString)
            .AddRedis(redisConnectionString)
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddSingleton<InMemoryMessageQueue>();
        services.AddTransient<IEventBus, EventBus>();
        services.AddHostedService<IntegrationEventProcessorJob>();
    }
}
