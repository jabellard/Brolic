using System.Threading.Tasks;
using Brolic.Abstractions;

namespace Brolic.Middlewares
{
    public class TrafficDispatchingMiddleware: IBrolicMiddleware
    {
        public Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}