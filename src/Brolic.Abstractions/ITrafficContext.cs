using Microsoft.AspNetCore.Http;

namespace Brolic.Abstractions
{
    public interface ITrafficContext
    {
        HttpContext HttpContext { get; }
        BrolicRoute Route { get; }
        Downstream Downstream { get;}
    }
}