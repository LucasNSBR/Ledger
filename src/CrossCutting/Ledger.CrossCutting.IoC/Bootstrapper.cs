using Ledger.Activations.Data.Context;
using Ledger.Activations.Domain.Context;
using Ledger.Companies.Data.Context;
using Ledger.Companies.Domain.Context;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Domain.Context;
using Ledger.Shared.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            InitializeCore(services);
            InitializeInfrastructure(services);

            ServiceBusBootstrapper.Initialize(services, configuration);
            EmailServiceBootstrapper.Initialize(services, configuration);
            IdentityBootstrapper.Initialize(services, configuration);
            ActivationContextBootstrapper.Initialize(services);
            CompanyContextBootstrapper.Initialize(services);
            HelpDeskContextBootstrapper.Initialize(services);
        }

        private static void InitializeCore(IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        }

        private static void InitializeInfrastructure(IServiceCollection services)
        {
            services.AddScoped<ILedgerActivationDbAbstraction, LedgerActivationDbContext>(provider => provider.GetRequiredService<LedgerActivationDbContext>());
            services.AddScoped<ILedgerCompanyDbAbstraction, LedgerCompanyDbContext>(provider => provider.GetRequiredService<LedgerCompanyDbContext>());
            services.AddScoped<ILedgerHelpDeskDbAbstraction, LedgerHelpDeskDbContext>(provider => provider.GetRequiredService<LedgerHelpDeskDbContext>());

            services.AddScoped<IUnitOfWork<ILedgerActivationDbAbstraction>, UnitOfWork<ILedgerActivationDbAbstraction>>();
            services.AddScoped<IUnitOfWork<ILedgerCompanyDbAbstraction>, UnitOfWork<ILedgerCompanyDbAbstraction>>();
            services.AddScoped<IUnitOfWork<ILedgerHelpDeskDbAbstraction>, UnitOfWork<ILedgerHelpDeskDbAbstraction>>();
        }
    }
}
