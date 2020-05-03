using RabbitMQ.Client;

namespace Brolic.Features.RabbitMQ.Abstractions
{
    public interface IRabbitMqConnectionProvider
    {
        IConnection GetRabbitMqConnection(string key);
    }
}