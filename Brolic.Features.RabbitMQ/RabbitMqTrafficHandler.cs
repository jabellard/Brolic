using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.RabbitMQ.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqTrafficHandler: ITrafficHandler
    {
        private readonly IRabbitMqRequestBuilder _rabbitMqRequestBuilder;
        private readonly IRabbitMqRequestExecutor _rabbitMqRequestExecutor;

        public RabbitMqTrafficHandler(IRabbitMqRequestBuilder rabbitMqRequestBuilder,
            IRabbitMqRequestExecutor rabbitMqRequestExecutor)
        {
            _rabbitMqRequestBuilder = rabbitMqRequestBuilder;
            _rabbitMqRequestExecutor = rabbitMqRequestExecutor;
        }
        public async  Task HandleTraffic(ITrafficContext trafficContext)
        {
            var rabbitMqRequest = await _rabbitMqRequestBuilder.BuildRabbitMqRequest(trafficContext);
            await _rabbitMqRequestExecutor.ExecuteRequest(rabbitMqRequest);
            trafficContext.HttpContext.Response.StatusCode = StatusCodes.Status202Accepted;
        }
    }
}