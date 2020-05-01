using System;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace Brolic
{
    public class RouteConfigurationProvider: IRouteConfigurationProvider
    {
        public Action<IRouteBuilder> GetRouteConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}