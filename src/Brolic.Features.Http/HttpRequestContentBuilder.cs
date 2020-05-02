using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Brolic.Features.Http.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.Http
{
    public class HttpRequestContentBuilder: IHttpRequestContentBuilder
    {
        public Task<HttpContent> BuildHttpRequestContent(HttpRequest httpRequest)
        {
            var httpRequestContent = new StreamContent(httpRequest.Body);
            httpRequestContent.Headers.ContentType = new MediaTypeHeaderValue(httpRequest.ContentType);
            return Task.FromResult((HttpContent) httpRequestContent);
        }
    }
}