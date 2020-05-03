using System.Threading.Tasks;
using Brolic.Features.RabbitMQ.Abstractions;

namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqRequestExecutor: IRabbitMqRequestExecutor
    {
        private readonly IRabbitMqConnectionProvider _rabbitMqConnectionProvider;

        public RabbitMqRequestExecutor(IRabbitMqConnectionProvider rabbitMqConnectionProvider)
        {
            _rabbitMqConnectionProvider = rabbitMqConnectionProvider;
        }
        
        public Task ExecuteRequest(RabbitMqRequest rabbitMqRequest)
        {
            var connection = _rabbitMqConnectionProvider.GetRabbitMqConnection(rabbitMqRequest.ConnectionKey);
            using var channel = connection.CreateModel();
            channel.BasicPublish(exchange: rabbitMqRequest.Exchange,
                routingKey:rabbitMqRequest.RoutingKey,
                mandatory: false,
                basicProperties: null,
                body: rabbitMqRequest.Payload);
            return Task.CompletedTask;
        }
    }
}