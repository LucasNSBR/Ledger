using Ledger.Activations.Application.AppServices.ActivationAppServices;
using Ledger.Activations.Data.Context;
using Ledger.Activations.Data.Repositories.ActivationRepository;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static class ActivationContextBootstrapper 
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddDbContext<LedgerActivationDbContext>(options =>
                options.UseInMemoryDatabase("ActivationDb"));

            InitializeFactories(services);
            InitializeRepositories(services);
            InitializeApplicationServices(services);
        }

        private static void InitializeFactories(IServiceCollection services)
        {
            services.AddScoped<IActivationFactory, ActivationFactory>();
        }

        private static void InitializeRepositories(IServiceCollection services)
        {
            services.AddScoped<IActivationRepository, ActivationRepository>();
        }

        private static void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IActivationApplicationService, ActivationApplicationService>();
        }
    }
}
