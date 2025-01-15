namespace EventBus.Base.SubscriptionInfos
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }

        public SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Create(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }

}
