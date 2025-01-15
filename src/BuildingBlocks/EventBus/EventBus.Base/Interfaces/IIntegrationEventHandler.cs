using EventBus.Base.Events;

namespace EventBus.Base.Interfaces
{
    public interface IIntegrationEventHandler<TIntegrationEvent> : IntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent integrationEvent);
    }

    public interface IntegrationEventHandler
    {
    }
}
