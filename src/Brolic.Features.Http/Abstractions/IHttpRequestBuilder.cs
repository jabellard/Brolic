using System.Net.Http;
using System.Threading.Tasks;
using Brolic.Abstractions;

namespace Brolic.Features.Http.Abstractions
{
    public interface IHttpRequestBuilder
    {
        Task<HttpRequestMessage> BuildHttpRequest(ITrafficContext trafficContext);
    }
}