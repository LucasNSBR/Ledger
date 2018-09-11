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
            CompanyContextBootstrapper.Initialize(services);
        }

        private static void InitializeCore(IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        }

        private static void InitializeInfrastructure(IServiceCollection services)
        {
            services.AddScoped<ILedgerActivationDbAbstraction, LedgerActivationDbContext>(provider => provider.GetRequiredService<LedgerActivationDbContext>());
            services.AddScoped<ILedgerCompanyDbAbstraction, LedgerCompanyDbContext>(provider => provider.GetRequiredService<LedgerCompanyDbContext>());

            services.AddScoped<IUnitOfWork<ILedgerActivationDbAbstraction>, UnitOfWork<ILedgerActivationDbAbstraction>>();
            services.AddScoped<IUnitOfWork<ILedgerCompanyDbAbstraction>, UnitOfWork<ILedgerCompanyDbAbstraction>>();
        }

        private static void InitializeBus(IServiceCollection services)
        {
            services.AddSingleton<IServiceBus, ServiceBus.ServiceBus>();
        }
    }
}
