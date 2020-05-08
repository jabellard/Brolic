using System.Threading.Tasks;
using Brolic.Abstractions;

namespace Brolic.Features.RateLimiting.Abstractions
{
    public interface ITrafficInitiatorIdentifier
    {
        public Task<string> IdentifyTrafficInitiator(ITrafficContext trafficContext);
    }
}