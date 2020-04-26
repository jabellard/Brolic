using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.Null.Abstractions;

namespace Brolic.Features.Null
{
    public class NullTrafficHandler: ITrafficHandler
    {
        private readonly INullResponseWriter _nullResponseWriter;

        public NullTrafficHandler(INullResponseWriter nullResponseWriter)
        {
            _nullResponseWriter = nullResponseWriter;
        }
        
        public Task HandleTraffic(ITrafficContext trafficContext)
        {
            var response = trafficContext.HttpContext.Response;
            _nullResponseWriter.WriteResponse(response);
            return Task.CompletedTask;
        }
    }
}