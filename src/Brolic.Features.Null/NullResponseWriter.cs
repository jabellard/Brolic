using Brolic.Features.Null.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.Null
{
    public class NullResponseWriter: INullResponseWriter
    {
        public void WriteResponse(HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status204NoContent;
        }
    }
}