using System.Collections.Generic;

namespace Brolic.Features.RabbitMQ.Options
{
    public class RabbitMqOptions
    {
        public List<KeyedRabbitMqConnectionInfo> KeyedConnections { get; set; }
    }
}