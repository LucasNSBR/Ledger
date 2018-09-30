using Ledger.Companies.Application.AppServices.CompanyAppServices;
using Ledger.Companies.Data.Context;
using Ledger.Companies.Data.Repositories.CompanyRepositories;
using Ledger.Companies.Domain.Context;
using Ledger.Companies.Domain.Factories.CompanyFactories;
using Ledger.Companies.Domain.Repositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.IoC.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Company Data Bounded Context
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="options">Optional configurations to setup context</param>
        /// <returns></returns>
        public static IServiceCollection AddCompanies(this IServiceCollection services, Action<CompanyContextOptions> setupAction = null)
        {
            services.AddDbContext<LedgerCompanyDbContext>(options =>
                options.UseInMemoryDatabase("CompanyDataDb"));

            services.AddScoped<ICompanyFactory, CompanyFactory>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyApplicationService, CompanyApplicationService>();

            services.AddScoped<ILedgerCompanyDbAbstraction, LedgerCompanyDbContext>(provider => provider.GetRequiredService<LedgerCompanyDbContext>());
            services.AddScoped<IUnitOfWork<ILedgerCompanyDbAbstraction>, UnitOfWork<ILedgerCompanyDbAbstraction>>();

            return services;
        }
    }
}
