using Ledger.CrossCutting.ServiceBus;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.CrossCutting.ServiceBus.BackgroundTasks;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static class ServiceBusBootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            InitializeDomainBus(services);
            InitializeMassTransit(services, configuration);
            InitializeHostedService(services);
        }

        //"Local" Domain Service Bus
        private static void InitializeDomainBus(IServiceCollection services)
        {
            services.AddScoped<IDomainServiceBus, DomainServiceBus>();
        }

        //Bounded Context Integration Service Bus
        private static void InitializeMassTransit(IServiceCollection services, IConfiguration configuration)
        {
            ServiceProvider provider = services.BuildServiceProvider();

            services.AddMassTransit(cfg =>
            {

            });

            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(transport =>
            {
                Uri hostAddress = new Uri(configuration["MassTransit:RabbitMqHost"]);

                transport.Host(hostAddress, host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                transport.ReceiveEndpoint("user_events", endpoint =>
                {
                    endpoint.LoadFrom(provider);
                });

                transport.ReceiveEndpoint("company_events", endpoint =>
                {
                    endpoint.LoadFrom(provider);
                });
            });

            services.AddSingleton(bus);
            services.AddSingleton<IIntegrationServiceBus, IntegrationServiceBus>();
        }

        private static void InitializeHostedService(IServiceCollection services)
        {
            services.AddHostedService<MassTransitProcess>();
        }
    }
}
