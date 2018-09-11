using Ledger.Activations.Data.Context;
using Ledger.Companies.Data.Context;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        //TODO: REFACTORE THIS
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            InitializeCore(services);
            InitializeInfrastructure(services);
            InitializeBus(services);

            EmailServiceBootstrapper.Initialize(services, configuration);
            IdentityBootstrapper.Initialize(services, configuration);
            ActivationContextBootstrapper.Initialize(services);
        }

        private static void InitializeCore(IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        }

        private static void InitializeInfrastructure(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<LedgerActivationDbContext>, UnitOfWork<LedgerActivationDbContext>>();
            services.AddScoped<IUnitOfWork<LedgerCompanyDbContext>, UnitOfWork<LedgerCompanyDbContext>>();
        }

        private static void InitializeBus(IServiceCollection services)
        {
            services.AddSingleton<IServiceBus, ServiceBus.ServiceBus>();
        }
    }
}
