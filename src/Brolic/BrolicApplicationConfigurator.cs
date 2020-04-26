using System;
using Brolic.Abstractions;

namespace Brolic
{
    public class BrolicApplicationConfigurator: IBrolicApplicationConfigurator
    {
        
        public IServiceProvider ApplicationServices { get;}
        public IBrolicApplicationConfiguration Configuration { get; }

        public BrolicApplicationConfigurator(IServiceProvider applicationServices)
        {
            ApplicationServices = applicationServices;
            Configuration = new BrolicApplicationConfiguration();
        }
        
        public IBrolicApplicationConfiguration Configure()
        {
            throw new NotImplementedException();
        }
    }
}