using Brolic.Features.RabbitMQ.Abstractions;
using RabbitMQ.Client;

namespace Brolic.Features.RabbitMQ
{
    public class KeyedRabbitMqConnection: IKeyedRabbitMqConnection
    {
        public KeyedRabbitMqConnection(string key, IConnection connection)
        {
            Key = key;
            Connection = connection;
        }
        public string Key { get; }
        public IConnection Connection { get; }
    }
}