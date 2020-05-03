using System.Collections.Generic;

namespace Brolic.Features.RabbitMQ
{
    public class KeyedRabbitMqConnectionInfo
    {
        public string Key { get; set; }
        public string HostName { get; set; }
        // TODO: Add configuration properties
        public List<RabbitMqExchange> Exchanges { get; set; }
    }
}