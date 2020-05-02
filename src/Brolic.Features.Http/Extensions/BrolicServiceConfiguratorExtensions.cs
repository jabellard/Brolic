using Brolic.Abstractions;

namespace Brolic.Features.Http.Extensions
{
    public static class BrolicServiceConfiguratorExtensions
    {
        public static IBrolicServiceConfigurator WithHttpFeature(this IBrolicServiceConfigurator brolicServiceConfigurator)
        {
            brolicServiceConfigurator.WithFeature<HttpFeature>();
            return brolicServiceConfigurator;
        }
    }
}