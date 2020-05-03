using System.Collections.Generic;
using System.Linq;
using Brolic.Features.RabbitMQ.Abstractions;
using RabbitMQ.Client;

namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqConnectionProvider: IRabbitMqConnectionProvider
    {
        private readonly IEnumerable<IKeyedRabbitMqConnection> _keyedRabbitMqConnections;

        public RabbitMqConnectionProvider(IEnumerable<IKeyedRabbitMqConnection> keyedRabbitMqConnections)
        {
            _keyedRabbitMqConnections = keyedRabbitMqConnections;
        }
        
        public IConnection GetRabbitMqConnection(string key)
        {
            return _keyedRabbitMqConnections
                .First(kc => kc.Key == key)
                .Connection;
        }
    }
}