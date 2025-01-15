using EventBus.Base.Configs;
using EventBus.Base.Interfaces;
using EventBus.RabbitMQ;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return config.EventBusType switch
            {
                "RabbitMQ" => new RabbitMQEventBus(config, serviceProvider)
            };
        }
    }

}
