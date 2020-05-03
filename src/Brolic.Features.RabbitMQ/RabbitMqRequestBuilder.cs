using System.IO;
using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.RabbitMQ.Abstractions;
using Brolic.Features.RabbitMQ.Extensions;
using Newtonsoft.Json;

namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqRequestBuilder: IRabbitMqRequestBuilder
    {
        public async Task<RabbitMqRequest> BuildRabbitMqRequest(ITrafficContext trafficContext)
        {
            var requestBody = trafficContext.HttpContext.Request.Body;
            requestBody.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(requestBody);
            var requestBodyText = await streamReader.ReadToEndAsync();
            var rabbitMqRequest = JsonConvert.DeserializeObject<RabbitMqRequest>(requestBodyText);
            var rabbitMqDownstreamOptions = trafficContext.Downstream.GetRabbitMqDownstreamOptions();
            rabbitMqRequest.ConnectionKey = rabbitMqDownstreamOptions.ConnectionKey;
            return rabbitMqRequest;
        }
    }
}