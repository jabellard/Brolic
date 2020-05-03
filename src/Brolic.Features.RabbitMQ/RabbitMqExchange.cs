using System.Collections.Generic;

namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqExchange
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public Dictionary<string, object> Arguments { get; set; }
    }
}