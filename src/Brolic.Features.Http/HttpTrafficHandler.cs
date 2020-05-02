using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.Http.Abstractions;

namespace Brolic.Features.Http
{
    public class HttpTrafficHandler: ITrafficHandler
    {
        private readonly IHttpRequestBuilder _httpRequestBuilder;
        private readonly IHttpRequestExecutor _httpRequestExecutor;
        private readonly IHttpResponseBuilder _httpResponseBuilder;

        public HttpTrafficHandler(IHttpRequestBuilder httpRequestBuilder, IHttpRequestExecutor httpRequestExecutor,
            IHttpResponseBuilder httpResponseBuilder)
        {
            _httpRequestBuilder = httpRequestBuilder;
            _httpRequestExecutor = httpRequestExecutor;
            _httpResponseBuilder = httpResponseBuilder;
        }
        public async Task HandleTraffic(ITrafficContext trafficContext)
        {
            var httpRequestMessage = await _httpRequestBuilder.BuildHttpRequest(trafficContext);
            var httpResponseMessage = await _httpRequestExecutor.ExecuteRequest(httpRequestMessage);
            var httpResponse = trafficContext.HttpContext.Response;
            await _httpResponseBuilder.BuildHttpResponse(httpResponse, httpResponseMessage);
        }
    }
}