using Brolic.Abstractions;
using Brolic.Features.Http.Options;

namespace Brolic.Features.Http.Extensions
{
    public static class DownStreamExtensions
    {
        public static HttpOptions GetHttpOptions(this Downstream downstream)
        {
            return downstream.Configuration as HttpOptions;
        }
    }
}