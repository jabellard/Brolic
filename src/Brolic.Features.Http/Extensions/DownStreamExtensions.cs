using Brolic.Abstractions;
using Brolic.Features.Http.Options;

namespace Brolic.Features.Http.Extensions
{
    public static class DownStreamExtensions
    {
        public static HttpDownstreamOptions GetHttpDownstreamOptions(this Downstream downstream)
        {
            return downstream.Data["Options"] as HttpDownstreamOptions;
        }
    }
}