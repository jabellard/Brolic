using System;
using System.Net.Http;
using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.Http.Abstractions;
using Brolic.Features.Http.Extensions;

namespace Brolic.Features.Http
{
    public class HttpRequestBuilder: IHttpRequestBuilder
    {
        private readonly IHttpRequestUriBuilder _httpRequestUriBuilder;
        private readonly IHttpRequestContentBuilder _httpRequestContentBuilder;

        public HttpRequestBuilder(IHttpRequestUriBuilder httpRequestUriBuilder,
            IHttpRequestContentBuilder httpRequestContentBuilder)
        {
            _httpRequestUriBuilder = httpRequestUriBuilder;
            _httpRequestContentBuilder = httpRequestContentBuilder;
        }
        
        public async Task<HttpRequestMessage> BuildHttpRequest(ITrafficContext trafficContext)
        {
            var httpContext = trafficContext.HttpContext;
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = await _httpRequestUriBuilder.BuildHttpRequestUri(trafficContext)
            };

            var requestMethod = httpContext.Request.Method.ToLower();
            var buildHttpRequestContent = false;
            switch (requestMethod)
            {
                case "get":
                    httpRequestMessage.Method = HttpMethod.Get;
                    break;
                case "post":
                    httpRequestMessage.Method = HttpMethod.Post;
                    buildHttpRequestContent = true;
                    break;
                case "put":
                    httpRequestMessage.Method = HttpMethod.Put;
                    buildHttpRequestContent = true;
                    break;
                case "patch":
                    httpRequestMessage.Method = HttpMethod.Patch;
                    buildHttpRequestContent = true;
                    break;
                case "delete":
                    httpRequestMessage.Method = HttpMethod.Delete;
                    break;
                case "head":
                    httpRequestMessage.Method = HttpMethod.Head;
                    break;
                case "options":
                    httpRequestMessage.Method = HttpMethod.Options;
                    break;
                case "trace":
                    httpRequestMessage.Method = HttpMethod.Trace;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var (key, value) in httpContext.Request.Headers)
                httpRequestMessage.Headers.TryAddWithoutValidation(key, value.ToArray());

            if (!buildHttpRequestContent) 
                return httpRequestMessage;
            
            using var httpRequestContent = await _httpRequestContentBuilder.BuildHttpRequestContent(httpContext.Request);
            httpRequestMessage.Content = httpRequestContent;

            return httpRequestMessage;
        }
    }
}