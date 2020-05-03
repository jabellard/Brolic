using System.Threading.Tasks;

namespace Brolic.Features.RabbitMQ.Abstractions
{
    public interface IRabbitMqRequestExecutor
    {
        Task ExecuteRequest(RabbitMqRequest rabbitMqRequest);
    }
}