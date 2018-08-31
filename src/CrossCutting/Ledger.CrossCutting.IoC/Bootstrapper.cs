using Ledger.CrossCutting.Data.Transactions;
using Ledger.Shared.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void Initialize(IServiceCollection services)
        {
            InitializeCore(services);
            InitializeInfrastructure(services);

            ActivationContextBootstrapper.Initialize(services);
        }

        private static void InitializeCore(IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        }

        private static void InitializeInfrastructure(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
