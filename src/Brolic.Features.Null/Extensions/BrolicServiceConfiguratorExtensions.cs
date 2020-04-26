using Brolic.Abstractions;

namespace Brolic.Features.Null.Extensions
{
    public static class BrolicServiceConfiguratorExtensions
    {
        public static IBrolicServiceConfigurator WithNullFeature(this IBrolicServiceConfigurator brolicServiceConfigurator)
        {
            brolicServiceConfigurator.WithFeature<NullFeature>();
            return brolicServiceConfigurator;
        }
    }
}