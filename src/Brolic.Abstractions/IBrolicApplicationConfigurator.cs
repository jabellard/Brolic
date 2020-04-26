using System;

namespace Brolic.Abstractions
{
    public interface IBrolicApplicationConfigurator
    {
        IServiceProvider ApplicationServices { get;}
        IBrolicApplicationConfiguration Configuration { get; }

        IBrolicApplicationConfiguration Configure();
    }
}