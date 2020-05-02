using Microsoft.AspNetCore.Http;

namespace Brolic.Abstractions
{
    public interface ITrafficContext
    {
        HttpContext HttpContext { get; }
        Downstream Downstream { get;}
    }
}