using ContentManager.Data.Entities;
using ContentManager.Domain.Interfaces;
using ContentManager.Domain.Services;
using ContentManager.Domain.Services.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManager.Domain.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoadDomainServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IReminderService, ReminderService>()
                .AddScoped<INotificationService, NotificationService>()
                .AddScoped<IPublishingService, PublishingService>()
                .AddScoped<IMessagingClient<Reminder>, ReminderMessagingClient>();
        }
    }
}
