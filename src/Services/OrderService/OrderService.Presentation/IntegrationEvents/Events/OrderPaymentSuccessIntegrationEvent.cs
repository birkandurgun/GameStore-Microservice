﻿using EventBus.Base.Events;

namespace OrderService.Presentation.IntegrationEvents.Events
{
    public class OrderPaymentSuccessIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderPaymentSuccessIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
