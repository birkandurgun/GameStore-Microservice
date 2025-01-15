using EventBus.Base.Events;

namespace EventBus.Base.Interfaces
{
    public interface IEventBus : IDisposable
    {
        void Publish(IntegrationEvent integrationEvent);
        void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IIntegrationEventHandler<TEvent>;
        void Unsubscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IIntegrationEventHandler<TEvent>;
    }

}
