using System;
using System.Threading.Tasks;
using Brolic.Abstractions;

namespace Brolic
{
    public class MainBrolicMiddleware: IBrolicMiddleware
    {
        public Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}