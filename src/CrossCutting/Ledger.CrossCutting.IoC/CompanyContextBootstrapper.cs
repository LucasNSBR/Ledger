using Ledger.Companies.Application.AppServices.CompanyAppServices;
using Ledger.Companies.Data.Context;
using Ledger.Companies.Data.Repositories.CompanyRepositories;
using Ledger.Companies.Domain.Factories.CompanyFactories;
using Ledger.Companies.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static class CompanyContextBootstrapper
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddDbContext<LedgerCompanyDbContext>(options =>
                options.UseInMemoryDatabase("CompanyDataDb"));

            InitializeFactories(services);
            InitializeRepositories(services);
            InitializeApplicationServices(services);
        }

        private static void InitializeFactories(IServiceCollection services)
        {
            services.AddScoped<ICompanyFactory, CompanyFactory>();
        }

        private static void InitializeRepositories(IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }

        private static void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<ICompanyApplicationService, CompanyApplicationService>();
        }
    }
}
