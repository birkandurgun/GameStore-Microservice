using EventBus.Base.Configs;
using EventBus.Base.Interfaces;
using EventBus.Factory;
using LibraryService.Api.IntegrationEvents.EventHandlers;
using LibraryService.Api.IntegrationEvents.Events;
using RabbitMQ.Client;

namespace LibraryService.Api.EventBusConfigurations
{
    public static class EventBusConfigurations
    {
        public static void AddEventBusConfigurations(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "LibraryService",
                    EventBusType = "RabbitMQ",
                    EventNameSuffix = "IntegrationEvent"
                };

                return EventBusFactory.Create(config, sp);
            });

            services.AddTransient<OrderCompletedIntegrationEventHandler>();
            
        }

        public static void AddEventBusSubscription(this IServiceProvider serviceProvider)
        {
            try
            {
                var eventBus = serviceProvider.GetRequiredService<IEventBus>();

                eventBus.Subscribe<OrderCompletedIntegrationEvent, OrderCompletedIntegrationEventHandler>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Subscription Error: {ex.Message}");
            }
        }
    }
}
