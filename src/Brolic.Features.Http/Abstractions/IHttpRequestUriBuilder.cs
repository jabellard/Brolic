using System;
using System.Threading.Tasks;
using Brolic.Abstractions;

namespace Brolic.Features.Http.Abstractions
{
    public interface IHttpRequestUriBuilder
    {
        Task<Uri> BuildHttpRequestUri(ITrafficContext trafficContext);
    }
}