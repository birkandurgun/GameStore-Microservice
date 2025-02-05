using EventBus.Base.Configs;
using EventBus.Base.Interfaces;
using EventBus.Base.SubscriptionManagers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EventBus.Base.Events
{
    public abstract class BaseEventBus : IEventBus
    {
        public readonly IServiceProvider ServiceProvider;
        public readonly IEventBusSubscriptionManager _subManager;

        public EventBusConfig _config { get; set; }

        protected BaseEventBus(EventBusConfig config, IServiceProvider serviceProvider)
        {
            _config = config;
            ServiceProvider = serviceProvider;
            _subManager = new EventBusSubscriptionManager(ProcessEventName);
        }

        public virtual string ProcessEventName(string eventName)
        {
            if (_config.DeleteEventPrefix && eventName.StartsWith(_config.EventNamePrefix))
            {
                eventName = eventName.Substring(_config.EventNamePrefix.Length);
            }

            if (_config.DeleteEventSuffix && eventName.EndsWith(_config.EventNameSuffix))
            {
                eventName = eventName.Substring(0, eventName.Length - _config.EventNameSuffix.Length);
            }

            return eventName;
        }

        public virtual string GetSubname(string eventName)
        {
            return $"{_config.SubscriberClientAppName}.{ProcessEventName(eventName)}";
        }

        public virtual void Dispose()
        {
            _config = null;
            _subManager.Clear();
        }

        public async Task<bool> ProcessEvent(string eventName, string message)
        {
            eventName = ProcessEventName(eventName);

            bool processed = false;

            if (_subManager.HasSubscriptionsForEvent(eventName))
            {
                var subs = _subManager.GetHandlersForEvent(eventName);

                using (var scope = ServiceProvider.CreateScope())
                {
                    foreach (var sub in subs)
                    {
                        var handler = ServiceProvider.GetService(sub.HandlerType);
                        if (handler == null) continue;

                        var eventType = _subManager.GetEventTypeByName
                            ($"{_config.EventNamePrefix}{eventName}{_config.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
                processed = true;
            }
            return processed;
        }

        public abstract void Publish(IntegrationEvent integrationEvent);

        public abstract void Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;

        public abstract void Unsubscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;
    }

}
