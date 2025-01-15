using EventBus.Base.Events;
using EventBus.Base.Interfaces;
using EventBus.Base.SubscriptionInfos;

namespace EventBus.Base.SubscriptionManagers
{
    public class EventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public bool IsEmpty => !_handlers.Keys.Any();
        public event EventHandler<string> OnEventRemoved;
        public Func<string, string> eventNameGetter;

        public EventBusSubscriptionManager(Func<string, string> eventNameGetter)
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
            this.eventNameGetter = eventNameGetter;
        }
        public void Clear() => _handlers.Clear();

        public void AddSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();

            AddSubscription(typeof(THandler), eventName);

            if (!_eventTypes.Contains(typeof(TEvent)))
                _eventTypes.Add(typeof(TEvent));
        }

        private void AddSubscription(Type handlerType, string eventName)
        {
            if (!HasSubscriptionsForEvent(eventName))
                _handlers.Add(eventName, new List<SubscriptionInfo>());

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
                throw new ArgumentException
                    ($"Handler type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));

            _handlers[eventName].Add(SubscriptionInfo.Create(handlerType));
        }

        public void RemoveSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var handlerToRemove = FindSubscriptionToRemove<TEvent, THandler>();
            var eventName = GetEventKey<TEvent>();
            RemoveHandler(eventName, handlerToRemove);
        }

        private void RemoveHandler(string eventName, SubscriptionInfo subscriptionToRemove)
        {
            if (subscriptionToRemove != null)
            {
                _handlers[eventName].Remove(subscriptionToRemove);

                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                        _eventTypes.Remove(eventType);
                }
                RaiseOnEventRemoved(eventName);
            }
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : IntegrationEvent
        {
            var key = GetEventKey<TEvent>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
            => _handlers[eventName];

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        public string GetEventKey<TEvent>()
        {
            string eventName = typeof(TEvent).Name;
            return eventNameGetter(eventName);
        }

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public bool HasSubscriptionsForEvent<TEvent>() where TEvent : IntegrationEvent
        {
            var key = GetEventKey<TEvent>();
            return HasSubscriptionsForEvent(key);
        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        private SubscriptionInfo FindSubscriptionToRemove<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            return FindSubscriptionToRemove(eventName, typeof(THandler));
        }

        private SubscriptionInfo FindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
                return null;

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }

    }

}
