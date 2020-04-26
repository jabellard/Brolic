using System.Threading.Tasks;

namespace Brolic.Abstractions
{
    public interface ITrafficHandler
    {
        Task HandleTraffic(ITrafficContext trafficContext);
    }
}