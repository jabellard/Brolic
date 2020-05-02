using System.Threading.Tasks;
using Brolic.Abstractions;

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
            var downstream = trafficContext.Downstream;
            var trafficHandler = _trafficHandlerProvider.GetTrafficHandler(downstream.Handler);
            await trafficHandler.HandleTraffic(trafficContext);
        }
    }
}