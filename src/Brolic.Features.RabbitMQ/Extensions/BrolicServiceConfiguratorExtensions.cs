using Brolic.Abstractions;

namespace Brolic.Features.RabbitMQ.Extensions
{
    public static class BrolicServiceConfiguratorExtensions
    {
        public static IBrolicServiceConfigurator WithRabbitMqFeature(this IBrolicServiceConfigurator brolicServiceConfigurator)
        {
            brolicServiceConfigurator.WithFeature<RabbitMqFeature>();
            return brolicServiceConfigurator;
        }
    }
}