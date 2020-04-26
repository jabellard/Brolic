using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Abstractions
{
    public interface IBrolicServiceConfigurator
    {
        IServiceCollection Services { get; }
        IBrolicServiceConfigurator WithFeature<TFeature>()
            where TFeature: IFeature;
    }
}