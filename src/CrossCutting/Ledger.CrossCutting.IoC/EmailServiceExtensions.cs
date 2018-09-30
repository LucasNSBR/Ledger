using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Services.Dispatchers;
using Ledger.CrossCutting.EmailService.Services.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Email Service
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="setupAction">List of configurations to setup email service</param>
        /// <returns></returns>
        public static IServiceCollection AddEmailService(this IServiceCollection services, Action<DispatcherOptions> setupAction)
        {
            services.Configure(setupAction); 

            services.AddScoped<IEmailFactory, EmailFactory>();
            services.AddScoped<IEmailDispatcher, EmailDispatcher>();

            return services;
        }
    }
}
