﻿using EventBus.Base.Events;

namespace PaymentProcessor.IntegrationEvents.Events
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public string ErrorMessage { get; set; }

        public OrderPaymentFailedIntegrationEvent(Guid orderId, string errorMessage)
        {
            OrderId = orderId;
            ErrorMessage = errorMessage;
        }
    }
}
