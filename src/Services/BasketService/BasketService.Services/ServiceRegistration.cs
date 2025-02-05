using BasketService.Entities.IntegrationEvents;
using BasketService.Services.IntegrationEventHandlers;
using BasketService.Services.Interfaces;
using BasketService.Services.Services;
using EventBus.Base.Configs;
using EventBus.Base.Interfaces;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IBasketService, BasketService.Services.Services.BasketService>();
            services.AddTransient<IUserContextService, UserContextService>();

            services.AddHttpContextAccessor();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "BasketService",
                    EventBusType = "RabbitMQ",
                    EventNameSuffix = "IntegrationEvent"
                };

                return EventBusFactory.Create(config, sp);
            });

            services.AddTransient<CheckOutIntegrationEventHandler>();
        }

        public static void AddEventBusSubscription(this IServiceProvider serviceProvider)
        {
            try
            {
                var eventBus = serviceProvider.GetRequiredService<IEventBus>();

                eventBus.Subscribe<CheckOutIntegrationEvent, CheckOutIntegrationEventHandler>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Subscription Error: {ex.Message}");
            }
        }
    }
}
