using System.Threading.Tasks;

namespace Brolic.Abstractions
{
    public interface IBrolicMiddleware
    {
        Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next);
    }
}