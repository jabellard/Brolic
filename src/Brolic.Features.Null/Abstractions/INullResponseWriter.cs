using Microsoft.AspNetCore.Http;

namespace Brolic.Features.Null.Abstractions
{
    public interface INullResponseWriter
    {
        void WriteResponse(HttpResponse response);
    }
}