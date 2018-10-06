using Ledger.CrossCutting.Storage.Configuration;
using Ledger.CrossCutting.Storage.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Storage Service
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="setupAction">List of configurations to Azure Blob Storage</param>
        /// <returns></returns>
        public static IServiceCollection AddEmailService(this IServiceCollection services, Action<StorageOptions> setupAction)
        {
            services.Configure(setupAction);

            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
    }
}
