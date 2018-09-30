using GreenPipes;
using Ledger.Activations.Domain.IntegrationEventHandlers.ActivationAggregate;
using Ledger.Companies.Domain.IntegrationEventHandlers.CompanyAggregate;
using Ledger.CrossCutting.IoC.Configuration;
using Ledger.CrossCutting.ServiceBus;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.CrossCutting.ServiceBus.BackgroundTasks;
using Ledger.HelpDesk.Domain.IntegrationEventHandlers.RoleAggregate;
using Ledger.HelpDesk.Domain.IntegrationEventHandlers.UserAggregate;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Service bus
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="setupAction">Optional configurations to setup service bus</param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBus(this IServiceCollection services, Action<ServiceBusOptions> setupAction = null)
        {
            ServiceBusOptions options = new ServiceBusOptions();
            setupAction.Invoke(options);

            services.AddScoped<IDomainServiceBus, DomainServiceBus>();

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<CompanyIntegrationEventHandler>();
                cfg.AddConsumer<ActivationIntegrationEventHandler>();
                cfg.AddConsumer<UserIntegrationEventHandler>();
                cfg.AddConsumer<RoleIntegrationEventHandler>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(transport =>
            {
                Uri hostAddress = new Uri(options.HostAddress);

                IRabbitMqHost host = transport.Host(hostAddress, cfg =>
                {
                    cfg.Username(options.RabbitMqHostUser);
                    cfg.Password(options.RabbitMqHostPassword);
                });

                transport.ReceiveEndpoint(host, "company_events", endpoint =>
                {
                    endpoint.LoadFrom(provider);
                    endpoint.UseRetry(r => r.Immediate(5));
                });
            }));

            services.AddSingleton<IIntegrationServiceBus, IntegrationServiceBus>();
            services.AddHostedService<MassTransitProcess>();

            return services;
        }
    }
}
