using System.Threading.Tasks;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic
{
    public class TrafficRouter: ITrafficRouter
    {
        public Task RouteTraffic(HttpContext httpContext)
        {
            throw new System.NotImplementedException();
        }
    }
}