using System.Net.Http;
using System.Threading.Tasks;

namespace Brolic.Features.Http.Abstractions
{
    public interface IHttpRequestExecutor
    {
        Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage requestMessage);
    }
}