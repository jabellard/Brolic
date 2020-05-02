using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.Http.Abstractions
{
    public interface IHttpRequestContentBuilder
    {
        Task<HttpContent> BuildHttpRequestContent(HttpRequest httpRequest);
    }
}