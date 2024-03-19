using ContentManager.Data.Interfaces;
using ContentManager.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ContentManager.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoadDatabaseContext(this IServiceCollection services, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

            return services
                .AddDbContext<IApplicationContext, ApplicationContext>(options => options
                    .UseMySql(connectionString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors())
                .AddScoped<IReminderRepository, ReminderRepository>()
                .AddScoped<INotificationRepository, NotificationRepository>();
        }

        public static IServiceCollection LoadDatabaseContext(this IServiceCollection services, string connectionString, string migrationsAssemblyName)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
            return services
                .AddDbContext<IApplicationContext, ApplicationContext>(options => options
                    .UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly(migrationsAssemblyName))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors())
                .AddScoped<IReminderRepository, ReminderRepository>()
                .AddScoped<INotificationRepository, NotificationRepository>();
        }
    }
}
