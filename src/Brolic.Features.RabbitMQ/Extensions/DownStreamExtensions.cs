using Brolic.Abstractions;
using Brolic.Features.RabbitMQ.Options;

namespace Brolic.Features.RabbitMQ.Extensions
{
    public static class DownStreamExtensions
    {
        public static RabbitMqDownstreamOptions GetRabbitMqDownstreamOptions(this Downstream downstream)
        {
            return downstream.Data["Options"] as RabbitMqDownstreamOptions;
        }
    }
}