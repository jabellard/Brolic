using System.Threading.Tasks;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace Brolic.Middlewares
{
    public class TrafficRoutingMiddleware: IBrolicMiddleware
    {
        private readonly IRouter _router;

        public TrafficRoutingMiddleware(IRouter router)
        {
            _router = router;
        }
        public async Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next)
        {
            var routeContext = new RouteContext(trafficContext.HttpContext);
            routeContext.RouteData.Routers.Add(_router);
            await _router.RouteAsync(routeContext);
            await routeContext.Handler(routeContext.HttpContext);
            await next(trafficContext);
        }
    }
}