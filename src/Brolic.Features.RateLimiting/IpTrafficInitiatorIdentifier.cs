using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.RateLimiting.Abstractions;

namespace Brolic.Features.RateLimiting
{
    public class IpTrafficInitiatorIdentifier: ITrafficInitiatorIdentifier
    {
        public Task<string> IdentifyTrafficInitiator(ITrafficContext trafficContext)
        {
            var ipAddress = trafficContext.HttpContext.Connection.RemoteIpAddress.ToString();
            return Task.FromResult(ipAddress);
        }
    }
}