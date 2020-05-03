using Brolic.Abstractions;
using Brolic.Features.RabbitMQ.Abstractions;
using Brolic.Features.RabbitMQ.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Brolic.Features.RabbitMQ
{
    public class RabbitMqFeature: IFeature
    {
        public string Key => "RabbitMQ";
        private readonly IFeatureOptionsProvider _featureOptionsProvider;

        public RabbitMqFeature(IFeatureOptionsProvider featureOptionsProvider)
        {
            _featureOptionsProvider = featureOptionsProvider;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>();
            services.AddSingleton<IRabbitMqRequestBuilder, RabbitMqRequestBuilder>();
            services.AddSingleton<IRabbitMqRequestExecutor, RabbitMqRequestExecutor>();
            var rabbitMqOptions = _featureOptionsProvider.GetFeatureOptions<RabbitMqOptions>(Key);
            foreach (var keyedConnection in rabbitMqOptions.KeyedConnections)
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = keyedConnection.HostName
                };

                var connection = connectionFactory.CreateConnection();
                var keyedRabbitMqConnection = new KeyedRabbitMqConnection(keyedConnection.Key, connection);
                services.AddSingleton<IKeyedRabbitMqConnection>(sp => keyedRabbitMqConnection);

                using var channel = connection.CreateModel();

                foreach (var exchange in keyedConnection.Exchanges)
                    channel.ExchangeDeclare(exchange.Key, exchange.Type, exchange.Durable, exchange.AutoDelete,
                        exchange.Arguments);
            }
        }

        public void Configure(IBrolicApplicationConfigurator brolicApplicationConfigurator)
        {
            var applicationServices = brolicApplicationConfigurator.ApplicationServices;
            var trafficHandlerRegistrar = applicationServices
                .GetRequiredService<ITrafficHandlerRegistrar>();
            trafficHandlerRegistrar.RegisterTrafficHandler<RabbitMqTrafficHandler>(Key);

            var applicationLifetime = applicationServices
                .GetRequiredService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                var keyedRabbitMqConnections = applicationServices
                    .GetServices<IKeyedRabbitMqConnection>();
                foreach (var keyedRabbitMqConnection in keyedRabbitMqConnections)
                    keyedRabbitMqConnection.Connection.Close();
            });
        }
    }
}