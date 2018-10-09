using Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Identity Resolver
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityResolver(this IServiceCollection services)
        {
            services.AddScoped<IIdentityResolverService, IdentityResolverService>();

            return services;
        }
    }
}
