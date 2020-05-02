using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Brolic.Features.Http.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.Http
{
    public class HttpResponseBuilder: IHttpResponseBuilder
    {
        public async Task BuildHttpResponse(HttpResponse httpResponse, HttpResponseMessage httpResponseMessage)
        {
            httpResponse.StatusCode = (int) httpResponseMessage.StatusCode;

            foreach (var (key, value) in httpResponseMessage.Headers)
                httpResponse.Headers.Add(key, value.ToArray());
            
            var httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
            await httpResponse.WriteAsync(httpResponseMessageContent);
        }
    }
}