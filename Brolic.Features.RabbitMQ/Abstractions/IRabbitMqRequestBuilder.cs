using System.Threading.Tasks;
using Brolic.Abstractions;

namespace Brolic.Features.RabbitMQ.Abstractions
{
    public interface IRabbitMqRequestBuilder
    {
        Task<RabbitMqRequest> BuildRabbitMqRequest(ITrafficContext trafficContext);
    }
}