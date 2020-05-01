using System;
using Microsoft.AspNetCore.Routing;

namespace Brolic.Abstractions
{
    public interface IRouteConfigurationProvider
    {
        Action<IRouteBuilder> GetRouteConfiguration();
    }
}