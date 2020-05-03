namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqRequest
    {
        public string ConnectionKey { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public byte[] Payload { get; set; }
    }
}