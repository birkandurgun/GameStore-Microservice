using EventBus.Base.Configs;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class RabbitMQEventBus : BaseEventBus
    {
        RabbitMQPersistentConnection _persistentConnection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IModel _consumerChannel;

        public RabbitMQEventBus(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            if (config.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(_config.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                _connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);

            }
            else
                _connectionFactory = new ConnectionFactory();

            _persistentConnection = new RabbitMQPersistentConnection(_connectionFactory, config.ConnectionRetryCount);

            _consumerChannel = CreateConsumerChannel();

            _subManager.OnEventRemoved += _subManager_OnEventRemoved;
        }

        private void _subManager_OnEventRemoved(object? sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            _consumerChannel.QueueUnbind(
                queue: eventName,
                exchange: _config.DefaultTopicName,
                routingKey: eventName
                );

            if (_subManager.IsEmpty)
                _consumerChannel.Close();
        }

        public override void Publish(IntegrationEvent integrationEvent)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var policy = Policy.Handle<BrokerUnreachableException>()
                    .Or<SocketException>()
                    .WaitAndRetry(_config.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => { }
                );

            var eventName = integrationEvent.GetType().Name;
            eventName = eventName = ProcessEventName(eventName);

            _consumerChannel.ExchangeDeclare(
                exchange: _config.DefaultTopicName,
                type: "direct"
                );

            var message = JsonConvert.SerializeObject(integrationEvent);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = _consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2; //persistent

                _consumerChannel.QueueDeclare(queue: GetSubname(eventName),
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                _consumerChannel.QueueBind(
                    queue: GetSubname(eventName),
                    exchange: _config.DefaultTopicName,
                    routingKey: eventName
                    );

                _consumerChannel.BasicPublish(
                    exchange: _config.DefaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body
                    );
            });
        }

        public override void Subscribe<TEvent, THandler>()
        {
            var eventName = typeof(TEvent).Name;
            eventName = ProcessEventName(eventName);

            if (!_subManager.HasSubscriptionsForEvent(eventName))
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }
                _consumerChannel.QueueDeclare(
                    queue: GetSubname(eventName),
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                _consumerChannel.QueueBind(
                    queue: GetSubname(eventName),
                    exchange: _config.DefaultTopicName,
                    routingKey: eventName
                    );
            }

            _subManager.AddSubscription<TEvent, THandler>();
            StartBasicConsume(eventName);
        }

        public override void Unsubscribe<TEvent, THandler>()
        {
            _subManager.RemoveSubscription<TEvent, THandler>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: _config.DefaultTopicName,
                                    type: "direct");
            return channel;
        }

        private void StartBasicConsume(string eventName)
        {
            if (_consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: GetSubname(eventName),
                    autoAck: false,
                    consumer: consumer
                    );
            }
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            { }

            _consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }
    }

}
