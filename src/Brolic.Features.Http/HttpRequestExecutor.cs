using System.Net.Http;
using System.Threading.Tasks;
using Brolic.Features.Http.Abstractions;

namespace Brolic.Features.Http
{
    public class HttpRequestExecutor: IHttpRequestExecutor
    {
        private readonly HttpClient _httpClient;

        public HttpRequestExecutor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage requestMessage)
        {
            return await _httpClient.SendAsync(requestMessage);
        }
    }
}