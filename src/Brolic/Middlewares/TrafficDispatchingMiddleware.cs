using System.Threading.Tasks;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Middlewares
{
    public class TrafficDispatchingMiddleware: IBrolicMiddleware
    {
        private readonly ITrafficHandlerProvider _trafficHandlerProvider;

        public TrafficDispatchingMiddleware(ITrafficHandlerProvider trafficHandlerProvider)
        {
            _trafficHandlerProvider = trafficHandlerProvider;
        }
        
        public async Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next)
        {
            var endPoint = trafficContext.HttpContext.GetEndpoint();
            endPoint.RequestDelegate(trafficContext.HttpContext);
            var downstream = trafficContext.Downstream;
            var trafficHandler = _trafficHandlerProvider.GetTrafficHandler(downstream.Handler);
            await trafficHandler.HandleTraffic(trafficContext);
        }
    }
}