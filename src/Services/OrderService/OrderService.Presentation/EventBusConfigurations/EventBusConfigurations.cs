using EventBus.Base.Configs;
using EventBus.Base.Interfaces;
using EventBus.Factory;
using OrderService.Presentation.IntegrationEvents.EventHandlers;
using OrderService.Presentation.IntegrationEvents.Events;

namespace OrderService.Presentation.EventBusConfigurations
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
                    SubscriberClientAppName = "OrderService",
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
                eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Subscription Error: {ex.Message}");
            }
        }
    }
}
