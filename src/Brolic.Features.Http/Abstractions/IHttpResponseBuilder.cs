using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.Http.Abstractions
{
    public interface IHttpResponseBuilder
    {
        Task BuildHttpResponse(HttpResponse httpResponse, HttpResponseMessage httpResponseMessage);
    }
}