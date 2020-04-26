using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Brolic.Abstractions
{
    public interface ITrafficRouter
    {
        Task RouteTraffic(HttpContext httpContext);
    }
}