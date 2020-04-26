using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Abstractions
{
    public interface IFeature
    {
        string Key { get;}
        void ConfigureServices(IServiceCollection services);
        void Configure(IBrolicApplicationConfigurator brolicApplicationConfigurator);
    }
}