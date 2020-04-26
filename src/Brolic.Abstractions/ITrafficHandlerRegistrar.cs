using System;

namespace Brolic.Abstractions
{
    public interface ITrafficHandlerRegistrar
    {
        void RegisterTrafficHandler<TTrafficHandler>(string key)
            where TTrafficHandler: ITrafficHandler;
        Type ResolveTrafficHandler(string key);
    }
}