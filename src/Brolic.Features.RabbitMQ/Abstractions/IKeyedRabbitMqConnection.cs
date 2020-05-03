using RabbitMQ.Client;

namespace Brolic.Features.RabbitMQ.Abstractions
{
    public interface IKeyedRabbitMqConnection
    {
        string Key { get; }
        IConnection Connection { get; }
    }
}