using Ledger.Activations.Application.AppServices.ActivationAppServices;
using Ledger.Activations.Data.Context;
using Ledger.Activations.Data.Repositories.ActivationRepository;
using Ledger.Activations.Domain.Context;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
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
        /// Add Dependencies for Activation Bounded Context
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="options">Optional configurations to setup context</param>
        /// <returns></returns>
        public static IServiceCollection AddActivations(this IServiceCollection services, Action<ActivationContextOptions> setupAction = null)
        {
            services.AddDbContext<LedgerActivationDbContext>(options =>
                  options.UseInMemoryDatabase("ActivationDb"));
            
            services.AddScoped<IActivationFactory, ActivationFactory>();
            services.AddScoped<IActivationRepository, ActivationRepository>();
            services.AddScoped<IActivationApplicationService, ActivationApplicationService>();

            services.AddScoped<ILedgerActivationDbAbstraction, LedgerActivationDbContext>(provider => provider.GetRequiredService<LedgerActivationDbContext>());
            services.AddScoped<IUnitOfWork<ILedgerActivationDbAbstraction>, UnitOfWork<ILedgerActivationDbAbstraction>>();

            return services;
        }
    }
}
