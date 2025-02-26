﻿using EventBus.Base.Events;

namespace PaymentProcessor.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderCreatedIntegrationEvent()
        {
            
        }

        public OrderCreatedIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
