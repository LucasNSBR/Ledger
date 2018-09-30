using Ledger.Shared.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Shared Kernel
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <returns></returns>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();

            return services;
        }
    }
}
